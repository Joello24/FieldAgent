using FieldAgent.Core.Entities;
using FieldAgent.DAL;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FieldAgent.Tests;

public class SecurityClearanceRepositoryTests
{
    SecurityClearanceRepository db;
    DBFactory factory;

    SecurityClearance GoodSecurityClearance = new SecurityClearance()
    {
        SecurityClearanceId = 1,
        SecurityClearanceName = "None",
    };
    SecurityClearance OutOfBoundsSecurityClearance = new SecurityClearance()
    {
        SecurityClearanceId = 9,
        SecurityClearanceName = "Bad",
    };
    
    

    [SetUp]
    public void Setup()
    {
        DBFactory factory = new DBFactory();
        db = new SecurityClearanceRepository(factory);
        factory.GetDbContext().Database.ExecuteSqlRaw("TestSetKnownGoodState");
    }
    //Response<SecurityClearance> Get(int securityClearanceId);
    //Response<List<SecurityClearance>> GetAll();

    [Test]
    public void GetOutOfBoundsSecurityClearanceTest()
    {
        var actual = db.Get(9);
        Assert.IsFalse(actual.Success);
        Assert.AreNotEqual(actual.Data,OutOfBoundsSecurityClearance);
    }
    [Test]
    public void GetGoodSecurityClearanceTest()
    {
        var actual = db.Get(1);
        Assert.IsTrue(actual.Success);
        Assert.AreEqual(actual.Data.ToString(),GoodSecurityClearance.ToString());
    }
    [Test]
    public void GetAllSecurityClearancesTest()
    {
        var actual = db.GetAll();
        Assert.IsTrue(actual.Success);
        Assert.AreEqual(actual.Data.Count,5);
    }
}