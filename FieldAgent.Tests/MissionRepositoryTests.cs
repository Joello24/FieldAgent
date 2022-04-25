using System;
using FieldAgent.Core.Entities;
using FieldAgent.DAL;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FieldAgent.Tests;

public class MissionRepositoryTests
{
    public static readonly Guid tester1Guid = new Guid("8BF8A3AC-3427-4788-9642-E73DFD2DA162");
    MissionRepository db;
    DBFactory factory;

    private Mission ExistingMission = new Mission
    {
        MissionId = 1,
        AgencyId = 6,
        CodeName = "maecenas leo",
        StartDate = new DateTime(2001, 07, 26),
        ProjectedEndDate = new DateTime(2025, 02, 22),
        ActualEndDate = null,
        OperationalCost = (decimal) 3018317.99,
        Notes = "Mauris sit amet eros. Suspendisse accumsan tortor quis turpis."
    };
    private Mission InsertableMission = new Mission
    {
        AgencyId = 6,
        CodeName = "maecenas leo",
        StartDate = new DateTime(2001, 07, 26),
        ProjectedEndDate = new DateTime(2025, 02, 22),
        ActualEndDate = null,
        OperationalCost = (decimal) 3018317.99,
        Notes = "Mauris sit amet eros. Suspendisse accumsan tortor quis turpis."
    };
    
    [SetUp]
    public void Setup()
    {
        DBFactory factory = new DBFactory();
        db = new MissionRepository(factory);
        factory.GetDbContext().Database.ExecuteSqlRaw("TestSetKnownGoodState");
    }
    // Response<Mission> Insert(Mission mission);
    // Response Update(Mission mission);
    // Response Delete(int missionId);
    // Response<Mission> Get(int missionId);
    // Response<List<Mission>> GetByAgency(int agencyId);
    // Response<List<Mission>> GetByAgent(int agentId);
    [Test]
    public void GetGoodMissionByAgencyTest()
    {
        var actual = db.GetByAgency(1);
        Assert.AreEqual(7, actual.Data.Count);
    }
    [Test]
    public void GetBadMissionByAgencyTest()
    {
        var actual = db.GetByAgency(8);
        Assert.AreEqual(0, actual.Data.Count);
    }
    [Test]
    public void GetGoodMissionByAgentTest()
    {
        var actual = db.GetByAgent(1);
        Assert.AreEqual(3, actual.Data.Count);
    }
    [Test]
    public void GetBadMissionByAgentTest()
    {
        var actual = db.GetByAgent(1000);
        Assert.AreEqual(0, actual.Data.Count);
        Assert.IsFalse(actual.Success);
    }
    [Test]
    public void GetGoodMissionTest()
    {
        var actual = db.Get(1);
        Assert.AreEqual(ExistingMission.MissionId, actual.Data.MissionId);
    }
    [Test]
    public void GetBadMissionTest()
    {
        var actual = db.Get(1000);
        Assert.IsNull(actual.Data);
    }
    [Test]
    public void InsertGoodMissionTest()
    {
        var actual = db.Insert(InsertableMission);
        Assert.AreEqual(InsertableMission.ToString(), actual.Data.ToString());
        Assert.IsTrue(actual.Success);
    }
    [Test]
    public void InsertBadMissionTest()
    {
        var actual = db.Insert(ExistingMission);
        Assert.IsFalse(actual.Success);
    }
    [Test]
    public void UpdateGoodMissionTest()
    {
        var actual = db.Update(ExistingMission);
        Assert.IsTrue(actual.Success);
    }
    [Test]
    public void UpdateBadMissionTest()
    {
        var actual = db.Update(InsertableMission);
        Assert.IsFalse(actual.Success);
    }
    

}