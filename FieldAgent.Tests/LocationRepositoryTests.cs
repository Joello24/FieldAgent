using System;
using Azure;
using FieldAgent.Core.Entities;
using FieldAgent.DAL;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FieldAgent.Tests;

public class LocationRepositoryTests
{
    
    // public int LocationId { get; set; }
    // public int AgencyId { get; set; }
    // public string LocationName { get; set; }
    // public string Street1 { get; set; }
    // public string? Street2 { get; set; }
    // public string City { get; set; }
    // public string PostalCode { get; set; }
    // public string CountryCode { get; set; }
    
    LocationRepository db;
    DBFactory factory;

    Location KnownExistingLocation = new Location()
    {
        LocationId = 1,
        AgencyId = 1,
        LocationName = "HeadQuarters",
        Street1 = "94 Jenna Center",
        Street2 = null,
        City = "Yuzui",
        PostalCode = "54932",
        CountryCode = "CN"
    };
    Location KnownNotExistingLocation = new Location()
    {
        LocationId = 1000,
        AgencyId = 1000,
        LocationName = "HeadQuarters",
        Street1 = "94 Jenna Center",
        Street2 = null,
        City = "Yuzui",
        PostalCode = "54932",
        CountryCode = "CN"
    };
    Location GoodNewLocation = new Location()
    {
        AgencyId = 1,
        LocationName = "Shack in Woods",
        Street1 = "Woods avenue",
        Street2 = null,
        City = "Yuzui",
        PostalCode = "54932",
        CountryCode = "CN"
    };
    
    [SetUp]
    public void Setup()
    {
        DBFactory factory = new DBFactory();
        db = new LocationRepository(factory);
        factory.GetDbContext().Database.ExecuteSqlRaw("TestSetKnownGoodState");
    }
    
    // Response<Location> Insert(Location location);
    // Response Update(Location location);
    // Response Delete(int locationId);
    // Response<Location> Get(int locationId);
    // Response<List<Location>> GetByAgency(int agencyId);
    
    [Test]
    public void TestInsertBadLocation()
    {
        var actual = db.Insert(KnownExistingLocation);
        Assert.IsFalse(actual.Success);
    }
    [Test]
    public void TestInsertGoodLocation()
    {
        var actual = db.Insert(GoodNewLocation);
        Assert.IsTrue(actual.Success);
    }
    [Test]
    public void TestUpdateBadLocation()
    {
        var actual = db.Update(KnownNotExistingLocation);
        Assert.IsFalse(actual.Success);
    }
    [Test]
    public void TestUpdateGoodLocation()
    {
        KnownExistingLocation.City = "New York";
        var actual = db.Update(KnownExistingLocation);
        Assert.IsTrue(actual.Success);
    }
    [Test]
    public void TestDeleteBadLocation()
    {
        var actual = db.Delete(KnownNotExistingLocation.LocationId);
        Assert.IsFalse(actual.Success);
    }
    [Test]
    public void TestDeleteGoodLocation()
    {
        var actual = db.Delete(KnownExistingLocation.LocationId);
        Assert.IsTrue(actual.Success);
    }
    [Test]
    public void TestGetBadLocation()
    {
        var actual = db.Get(KnownNotExistingLocation.LocationId);
        Assert.IsFalse(actual.Success);
    }
    [Test]
    public void TestGetGoodLocation()
    {
        var actual = db.Get(KnownExistingLocation.LocationId);
        Assert.IsTrue(actual.Success);
    }
    [Test]
    public void TestGetByAgencyBadAgency()
    {
        var actual = db.GetByAgency(KnownNotExistingLocation.AgencyId);
        Assert.IsFalse(actual.Success);
    }
    [Test]
    public void TestGetByAgencyGoodAgency()
    {
        var actual = db.GetByAgency(KnownExistingLocation.AgencyId);
        Assert.IsTrue(actual.Success);
    }
}