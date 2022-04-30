using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels;

public class ViewAlias
{
    public int AliasId { get; set; }
    
    [Required]
    public int AgentId { get; set; }
    
    [StringLength(50, ErrorMessage = "Alias must be less than 50 characters")]
    public string AliasName { get; set; }

    public Guid? InterpolId { get; set; }
    
    public string Persona { get; set; }
    
    // AliasId int primary key identity(1,1),
    // AgentId int not null foreign key references Agent(AgentId),
    // AliasName nvarchar(50),
    // InterpolId UNIQUEIDENTIFIER null,
    // Persona ntext null
}