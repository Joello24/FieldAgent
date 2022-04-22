namespace FieldAgent.Core.Entities;

public class Mission
{
    // MissionId int primary key identity(1,1),
    // AgencyId int not null foreign key references Agency(AgencyId),
    // CodeName nvarchar(50) not null,
    // StartDate datetime2 not null,
    // ProjectedEndDate datetime2 not null,
    // ActualEndDate datetime2 null,
    // OperationalCost decimal(10,2),
    // Notes ntext null
    
    public int MissionId { get; set; }
    public int AgencyId { get; set; }
    public string CodeName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ProjectedEndDate { get; set; }
    public DateTime ActualEndDate { get; set; }
    public decimal OperationalCost { get; set; }
    public string? Notes { get; set; }
}