using System;
using FieldAgent.Core.Entities;
using FieldAgent.DAL;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FieldAgent.Tests;

public class AliasRepositoryTests
{
    AliasRepository db;
    DBFactory factory;

    Alias GoodAlias = new Alias()
    {
        AliasId = 1,
        AliasName = "Wrapsafe",
        AgentId = 65
    };
    Alias BadAlias = new Alias()
    {
        AliasName = "BadAlias",
        AgentId = 1000
    };
    Alias GoodInsert = new Alias()
    {
        AliasName = "Wrapsafe",
        AgentId = 65 
    };
    
    

    [SetUp]
    public void Setup()
    {
        DBFactory factory = new DBFactory();
        db = new AliasRepository(factory);
        factory.GetDbContext().Database.ExecuteSqlRaw("TestSetKnownGoodState");
    }
    // Response<Alias> Insert(Alias alias);
    // Response Update(Alias alias);
    // Response Delete(int aliasId);
    // Response<Alias> Get(int aliasId);
    // Response<List<Alias>> GetByAgent(int agentId);
    [Test]
    public void InsertBadAliasTest()
    {
        var actual = db.Insert(BadAlias);
        Assert.AreEqual(actual.Success, false);
    }
    [Test]
    public void InsertGoodAliasTest()
    {
        var actual = db.Insert(GoodInsert);
        Assert.AreEqual(actual.Success, true);
    }
    [Test]
    public void UpdateBadAliasTest()
    {
        var actual = db.Update(BadAlias);
        Assert.AreEqual(actual.Success, false);
    }
    [Test]
    public void UpdateGoodAliasTest()
    {
        var actual = db.Update(GoodAlias);
        Assert.AreEqual(actual.Success, true);
    }
    [Test]
    public void DeleteBadAliasTest()
    {
        var actual = db.Delete(BadAlias.AliasId);
        Assert.AreEqual(actual.Success, false);
    }
    [Test]
    public void DeleteGoodAliasTest()
    {
        var actual = db.Delete(GoodAlias.AliasId);
        Assert.AreEqual(actual.Success, true);
    }
    [Test]
    public void GetBadAliasTest()
    {
        var actual = db.Get(BadAlias.AliasId);
        Assert.AreEqual(actual.Success, false);
    }
    [Test]
    public void GetGoodAliasTest()
    {
        var actual = db.Get(GoodAlias.AliasId);
        Assert.AreEqual(actual.Success, true);
    }
    [Test]
    public void GetByAgentBadAliasTest()
    {
        var actual = db.GetByAgent(1000);
        Assert.AreEqual(actual.Success, false);
    }
    [Test]
    public void GetByAgentGoodAliasTest()
    {
        var actual = db.GetByAgent(GoodAlias.AgentId);
        Assert.AreEqual(actual.Success, true);
    }
    

}