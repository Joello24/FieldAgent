using System;
using FieldAgent.Core.Entities;
using FieldAgent.DAL;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FieldAgent.Tests;

public class AgencyRepositoryTests
{
    AgencyRepository db;
    DBFactory factory;

    private Agency tester1 = new Agency
    {
        AgencyId = 1,
        ShortName = "FBI",
        LongName = "Federal Bureau of Investigation"
    };
    private Agency tester2 = new Agency
    {
        ShortName = "FDA",
        LongName = "Federal Drug Administration"
    };
    private Agency tester3 = new Agency
    {
        AgencyId = 9,
        ShortName = "FDA",
        LongName = "Federal Drug Administration"
    };

    [SetUp]
    public void Setup()
    {
        DBFactory factory = new DBFactory();
        db = new AgencyRepository(factory);
        factory.GetDbContext().Database.ExecuteSqlRaw("TestSetKnownGoodState");
    }
    
    // Response<Agency> Insert(Agency agency);
    // Response Update(Agency agency);
    // Response Delete(int agencyId);
    // Response<Agency> Get(int agencyId);
    // Response<List<Agency>> GetAll();
    [Test]
    public void TestBadKeyInsertAgency()
    {
        var expected = db.Insert(tester1);
        Assert.AreEqual(expected.Data, tester1);
        Assert.IsFalse(expected.Success);
    }
    [Test]
    public void TestGoodKeyInsertAgency()
    {
        var expected = db.Insert(tester2);
        Assert.AreEqual(expected.Data, tester2);
        Assert.IsTrue(expected.Success);
    }
    [Test]
    public void TestBadKeyUpdateAgency()
    {
        var expected = db.Update(tester3);
        Assert.IsFalse(expected.Success);
    }
    [Test]
    public void TestGoodKeyUpdateAgency()
    {
        var expected = db.Update(tester1);
        Assert.IsTrue(expected.Success);
    }
    [Test]
    public void TestBadKeyDeleteAgency()
    {
        var expected = db.Delete(9);
        Assert.IsFalse(expected.Success);
    }
    [Test]
    // Deleting contraints + Agency.
    
    public void TestGoodKeyDeleteAgency() 
        
    {
        var expected = db.Delete(2);
        Assert.IsTrue(expected.Success);
    }
    [Test]
    public void TestBadKeyGetAgency()
    {
        var expected = db.Get(9);
        Assert.IsFalse(expected.Success);
    }
    [Test]
    public void TestGoodKeyGetAgency()
    {
        var expected = db.Get(1);
        Assert.IsTrue(expected.Success);
    }
    [Test]
    public void TestGetAllAgencies()
    {
        var expected = db.GetAll();
        Assert.IsTrue(expected.Success);
    }
}
    