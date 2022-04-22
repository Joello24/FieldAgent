namespace FieldAgent.Core.Entities;

public class Alias
{
    // AliasId int primary key identity(1,1),
    // AgentId int not null foreign key references Agent(AgentId),
    // AliasName nvarchar(50),
    // InterpolId UNIQUEIDENTIFIER null,
    // Persona ntext null
    public int AliasId { get; set; }
    public int AgentId { get; set; }
    public string AliasName { get; set; }
    public Guid? InterpolId { get; set; }
    public string? Persona { get; set; }
    
}