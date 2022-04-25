using System;
using FieldAgent.Core.Entities;
using FieldAgent.DAL;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FieldAgent.Tests;

public class AgencyAgentRepositoryTests
{
    public static readonly Guid tester1Guid = new Guid("8BF8A3AC-3427-4788-9642-E73DFD2DA162");
    AgencyAgentRepository db;
    DBFactory factory;
    AgencyAgent tester1 = new AgencyAgent
    {
        AgencyId = 1,
        AgentId = 1,
        SecurityClearanceId = 1,
        BadgeId = tester1Guid,
        ActivationDate = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0)),
        IsActive = true
    };
    AgencyAgent Bad = new AgencyAgent
    {
        AgencyId = 10,
        AgentId = 1,
        SecurityClearanceId = 1,
        BadgeId = tester1Guid,
        ActivationDate = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0)),
        IsActive = true
    };
    [SetUp]
    public void Setup()
    {
        DBFactory factory = new DBFactory();
        db = new AgencyAgentRepository(factory);
        factory.GetDbContext().Database.ExecuteSqlRaw("TestSetKnownGoodState");
    }

    
    // Response<AgencyAgent> Insert(AgencyAgent agencyAgent);
    // Response Update(AgencyAgent agencyAgent);
    // Response Delete(int agencyid, int agentid);
    // Response<AgencyAgent> Get(int agencyid, int agentid);
    // Response<List<AgencyAgent>> GetByAgency(int agencyId);
    // Response<List<AgencyAgent>> GetByAgent(int agentId);
    [Test]
    public void TestGetGoodAgencyAgent()
    {
        var actual = db.Get(1, 1);
        Assert.AreEqual(tester1.ToString(), actual.Data.ToString());
    }

    [Test]
    public void TestGetBadAgencyAgent()
    {
        var actual = db.Get(1000,1000);
        Assert.IsNull(actual.Data);
    }

    [Test]
    public void TestInsertGoodAgencyAgent()
    {
        var actual = db.Insert(tester1);
        Assert.AreEqual(true, actual.Success);
    }

    [Test]
    public void TestInsertBadAgencyAgent()
    {
        var actual = db.Insert(Bad);
        Assert.AreEqual(false, actual.Success);
    }
    [Test]
    public void TestUpdateGoodAgencyAgent()
    {
        var actual = db.Update(tester1);
        Assert.AreEqual(true, actual.Success);
    }
    [Test]
    public void TestUpdateBadAgencyAgent()
    {
        var actual = db.Update(Bad);
        Assert.AreEqual(false, actual.Success);
    }
    [Test]
    public void TestDeleteGoodAgencyAgent()
    {
        var actual = db.Delete(1, 1);
        Assert.AreEqual(true, actual.Success);
    }
    public void TestDeleteBadAgencyAgent()
    {
        var actual = db.Delete(1000, 1000);
        Assert.AreEqual(false, actual.Success);
    }
    [Test]
    public void TestGetByAgencyGood()
    {
        var actual = db.GetByAgency(1);
        Assert.AreEqual(true, actual.Success);
    }
    [Test]
    public void TestGetByAgencyBad()
    {
        var actual = db.GetByAgency(1000);
        Assert.AreEqual(false, actual.Success);
    }
    [Test]
    public void TestGetByAgentGood()
    {
        var actual = db.GetByAgent(1);
        Assert.AreEqual(true, actual.Success);
    }

    [Test]
    public void TestGetByAgentBad()
    {
        var actual = db.GetByAgent(1000);
        Assert.AreEqual(false, actual.Success);
    }
}