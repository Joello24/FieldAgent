namespace FieldAgent.Core.Entities;

public class MissionAgent
{
    // MissionId int not null foreign key references Mission(MissionId),
    // AgentId int not null foreign key references Agent(AgentId),
    // CONSTRAINT PK_MissionAgent PRIMARY KEY (MissionId, AgentId)
    
    public int MissionId { get; set; }
    public int AgentId { get; set; }
}