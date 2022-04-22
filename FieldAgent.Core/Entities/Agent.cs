namespace FieldAgent.Core.Entities;

public class Agent
{
    // AgentId int primary key identity(1,1),
    // FirstName nvarchar(50) not null,
    // LastName nvarchar(50) not null,
    // DateOfBirth datetime2 not null,
    // Height decimal(5,2) not null
    
    public int AgentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public decimal Height { get; set; }
    
}

