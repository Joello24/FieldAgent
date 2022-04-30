using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.ViewModels;

public class ViewAgent
{
    public int agentId { get; set; }
    
    
    [Required]
    [StringLength(50,ErrorMessage = "First Name cannot be longer than 50 characters.")]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(50,ErrorMessage = "Last Name cannot be longer than 50 characters.")]
    public string LastName { get; set; }
    
    [Required]
    public DateTime DateOfBirth { get; set; }
    
    [Required]
    [Precision(5,2)]
    public decimal Height { get; set; }
}