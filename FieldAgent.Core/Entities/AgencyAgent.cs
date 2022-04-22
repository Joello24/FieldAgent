namespace FieldAgent.Core.Entities;

public class AgencyAgent
{
    // AgencyId int not null foreign key references Agency(AgencyId),
    // AgentId int not null foreign key references Agent(AgentId),
    // SecurityClearanceId int not null foreign key references SecurityClearance(SecurityClearanceId),
    // BadgeId UNIQUEIDENTIFIER not null,
    // ActivationDate datetime2 not null,
    // DeactivationDate datetime2 null,
    // IsActive bit not null default(1)
    public int AgencyId { get; set; }
    public int AgentId { get; set; }
    public int SecurityClearanceId { get; set; }
    public string BadgeId { get; set; }
    public DateTime ActivationDate { get; set; }
    public DateTime? DeactivationDate { get; set; }
    public bool IsActive { get; set; }
}