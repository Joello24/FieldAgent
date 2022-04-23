using System;
using Azure;
using FieldAgent.Core.Entities;
using FieldAgent.DAL;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using FieldAgent.Core;

namespace FieldAgent.Tests;

public class AgentRepositoryTests
{
    AgentRepository db;
    DBFactory factory;

    private Agent AgentWithoutId = new Agent
    {
        FirstName = "Jim",
        LastName = "Halpert",
        DateOfBirth = new DateTime(1995,01,01),
        Height = 72
    };
    private Agent AgentWithId = new Agent
    {
        AgentId = 1,
        FirstName = "Jim",
        LastName = "Halpert",
        DateOfBirth = new DateTime(1995,01,01),
        Height = 72
    };

    [SetUp]
    public void Setup()
    {
        DBFactory factory = new DBFactory();
        db = new AgentRepository(factory);
        factory.GetDbContext().Database.ExecuteSqlRaw("TestSetKnownGoodState");
    }
    // Response<Agent> Insert(Agent agent);
    // Response Update(Agent agent);
    // Response Delete(int agentId);
    // Response<Agent> Get(int agentId);
    // Response<List<Mission>> GetMissions(int agentId);
    [Test]
    public void InsertGoodAgentTest()
    {
        var actual = db.Insert(AgentWithoutId);
        Assert.AreEqual(AgentWithoutId, actual.Data);
        Assert.IsTrue(actual.Success);
    }
    [Test]
    public void InsertBadAgentTest()
    {
        var actual = db.Insert(AgentWithId);
        Assert.AreEqual(AgentWithId, actual.Data);
        Assert.IsFalse(actual.Success);
    }
    [Test]
    public void UpdateGoodAgentTest()
    {
        var actual = db.Update(AgentWithId); 
        Assert.IsTrue(actual.Success);
    }
    [Test]
    public void UpdateBadAgentTest()
    {
        var actual = db.Update(AgentWithoutId); 
        Assert.IsFalse(actual.Success);
    }
    [Test]
    public void DeleteGoodAgentTest()
    {
        var actual = db.Delete(1);
        Assert.IsTrue(actual.Success);
        Assert.IsFalse(db.Get(1).Success);
    }
    [Test]
    public void DeleteBadAgentTest()
    {
        var actual = db.Delete(1000);
        Assert.IsFalse(actual.Success);
    }
    [Test]
    public void GetGoodAgentTest()
    {
        var actual = db.Get(1);
        Assert.AreEqual(AgentWithId.ToString(), actual.Data.ToString());
        Assert.IsTrue(actual.Success);
    }
    [Test]
    public void GetBadAgentTest()
    {
        var actual = db.Get(1000);
        Assert.IsFalse(actual.Success);
    }
    [Test]
    public void GetMissionsGoodAgentTest()
    {
        var actual = db.GetMissions(1);
        Assert.IsTrue(actual.Success);
    }
    [Test]
    public void GetMissionsBadAgentTest()
    {
        var actual = db.GetMissions(1000);
        Assert.IsFalse(actual.Success);
    }

}