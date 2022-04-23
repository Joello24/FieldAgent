using FieldAgent.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL;

public class AppDbContext : DbContext
{
    public DbSet<Agency> Agency { get; set; }
    public DbSet<Agent> Agent { get; set; }
    public DbSet<AgencyAgent> AgencyAgent { get; set; }
    public DbSet<Alias> Alias { get; set; }
    public DbSet<Location> Location { get; set; }
    public DbSet<Mission> Mission { get; set; }
    public DbSet<SecurityClearance> SecurityClearance { get; set; }
    
    public DbSet<MissionAgent> MissionAgent { get; set; }

    public AppDbContext() : base()
    {
    }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AgencyAgent>()
            .HasKey(agag => new { agag.AgencyId, agag.AgentId });
        modelBuilder.Entity<MissionAgent>()
            .HasKey(ma => new { ma.MissionId, ma.AgentId });
        // modelBuilder.Entity<Mission>()
        //     .HasMany(aMission => aMission.Agent)
        //     .WithMany(aAgent=> aAgent.Mission);
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }
}