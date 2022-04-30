using System.ComponentModel.DataAnnotations;
using FieldAgent.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.ViewModels;

public class ViewMission
{
    public int MissionId { get; set; }
    [Required]
    public int AgencyId { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Code name must be less than 50 characters")]
    public string CodeName { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime ProjectedEndDate { get; set; }
    public DateTime? ActualEndDate { get; set; }
    [Required]
    [Precision(10,2)]
    public decimal OperationalCost { get; set; }
    public string? Notes { get; set; }

    // AgencyId int not null foreign key references Agency(AgencyId),
    // CodeName nvarchar(50) not null,
    // StartDate datetime2 not null,
    // ProjectedEndDate datetime2 not null,
    // ActualEndDate datetime2 null,
    // OperationalCost decimal(10,2),
    // Notes ntext null
}