using Enrolly.Admissions.Application.Configurations;
using Enrolly.Admissions.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Admissions.Application;
    
public class AdmissionsDbContext(DbContextOptions<AdmissionsDbContext> options) 
    : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Admission> Admissions  { get; set; }
    public DbSet<AdmissionProgram> AdmissionPrograms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new AdmissionConfiguration());
        modelBuilder.ApplyConfiguration(new AdmissionProgramConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}