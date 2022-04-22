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
        // public int AgencyId { get; set; }
        // public int AgentId { get; set; }
        // public int SecurityClearanceId { get; set; }
        // public string BadgeId { get; set; }
        // public DateTime ActivationDate { get; set; }
        // public DateTime? DeactivationDate { get; set; }
        // public bool IsActive { get; set; }
        AgencyId = 1,
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
        factory.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
    }

    [Test]
    public void TestGetAgencyAgent()
    {
        var actual = db.Get(1, 1);
        Assert.AreEqual(tester1.ToString(), actual.Data.ToString());
    }
    
    
}