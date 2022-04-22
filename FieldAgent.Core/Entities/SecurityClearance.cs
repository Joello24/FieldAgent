namespace FieldAgent.Core.Entities;

public class SecurityClearance
{
    // SecurityClearanceId int primary key identity(1,1),
    // SecurityClearanceName varchar(50) not null
    public int SecurityClearanceId { get; set; }
    public string SecurityClearanceName { get; set; }
}