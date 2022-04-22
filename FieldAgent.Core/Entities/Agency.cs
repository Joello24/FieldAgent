namespace FieldAgent.Core.Entities;

public class Agency
{
    // AgencyId int primary key identity(1,1),
    // ShortName nvarchar(25) not null,
    // LongName nvarchar(255) null
    public int AgencyId { get; set; }
    public string ShortName { get; set; }
    public string LongName { get; set; }
}