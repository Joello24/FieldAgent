namespace FieldAgent.Core.Entities;

public class Location
{
    // LocationId int primary key identity(1,1),
    // AgencyId int not null foreign key references Agency(AgencyId),
    // LocationName nvarchar(50) not null,
    // Street1 nvarchar(50) not null,
    // Street2 nvarchar(50) null,
    // City nvarchar(50) not null,
    // PostalCode nvarchar(15) not null,
    // CountryCode varchar(5) not null
    public int LocationId { get; set; }
    public int AgencyId { get; set; }
    public string LocationName { get; set; }
    public string Street1 { get; set; }
    public string Street2 { get; set; }
    public string City { get; set; }
    public int PostalCode { get; set; }
    public string CountryCode { get; set; }
}