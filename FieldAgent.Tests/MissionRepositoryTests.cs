using System;
using FieldAgent.Core.Entities;
using FieldAgent.DAL;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FieldAgent.Tests;

public class MissionRepositoryTests
{
    public static readonly Guid tester1Guid = new Guid("8BF8A3AC-3427-4788-9642-E73DFD2DA162");
    AgencyAgentRepository db;
    DBFactory factory;

    private Mission mission1 = new Mission
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
    
    [SetUp]
    public void Setup()
    {
        DBFactory factory = new DBFactory();
        db = new AgencyAgentRepository(factory);
        factory.GetDbContext().Database.ExecuteSqlRaw("TestSetKnownGoodState");
    }
    [Test]
    public void GetMissionTest()
    {
        var actual = db.GetByAgency(1);
        Assert.AreEqual(8, actual.Data.Count);
    }

}