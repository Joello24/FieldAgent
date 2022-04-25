using System;
using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.DAL;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FieldAgent.Tests;

public class ReportsRepositoryTests
{
    public static readonly Guid guid1 = new Guid("0C29F462-76EF-4220-90C9-BAD635B32B5A");
    public static readonly Guid PensionListIndex1Guid = new Guid("8605CFAF-E241-4391-BA52-3B421614D93C");
    ReportsRepository db;
    DBFactory factory;

    private ClearanceAuditListItem auditListFirstRow = new ClearanceAuditListItem()
    {
        BadgeId = guid1,
        NameLastFirst = "Michaella Cleve",
        DateOfBirth = new DateTime(2010,04,12),
        ActivationDate = new DateTime(1995,01,16),
        DeactivationDate = null
    };

        [SetUp]
    public void Setup()
    {
        DBFactory factory = new DBFactory();
        var connectionString = factory.GetConnectionString();
        db = new ReportsRepository(connectionString);
        factory.GetDbContext().Database.ExecuteSqlRaw("TestSetKnownGoodState");
    }

    [Test]
    public void GetGoodPensionListTest()
    {
        var actual = db.GetPensionList(6);
        Assert.IsTrue(actual.Success);
        Assert.AreEqual(7, actual.Data.Count);
        Assert.AreEqual(PensionListIndex1Guid, actual.Data[0].BadgeId);
    }

    [Test]
    public void GetBadPensionListTest()
    {
        var actual = db.GetPensionList(8);
        Assert.IsFalse(actual.Success);
    }

    [Test] 
    public void GetTopAgentsTest()
    {
        var actual = db.GetTopAgents();
        Assert.IsTrue(actual.Success);
        Assert.AreEqual(5,actual.Data[0].CompletedMissionCount);
        Assert.AreEqual(3, actual.Data.Count);
    }
    [Test]
    public void GetTopAgentsTestIndex1()
    {
        var actual = db.GetTopAgents();
        Assert.IsTrue(actual.Success);
        Assert.AreEqual(3, actual.Data[1].CompletedMissionCount);
    }

    [Test]
    public void GetGoodAuditListTest()
    {
        var actual = db.AuditClearance(3, 1);
        Assert.IsTrue(actual.Success);
        Assert.AreEqual(auditListFirstRow.BadgeId,actual.Data[0].BadgeId);
    }
    [Test]
    public void GetBadAuditListTestBadAgency()
    {
        var actual = db.AuditClearance(3, 8);
        Assert.IsFalse(actual.Success);
    } 
    [Test]
    public void GetBadAuditListTestBadSecurityClearance()
    {
        var actual = db.AuditClearance(10, 3);
        Assert.IsFalse(actual.Success);
    } 
}