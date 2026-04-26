using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Admissions.Infrastructure.Database;
    
public class AdmissionsDbContext(DbContextOptions<AdmissionsDbContext> options) 
    : DbContext(options)
{
    public DbSet<Applicant> Applicants { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<Admission> Admissions  { get; set; }
    public DbSet<AdmissionProgram> AdmissionPrograms { get; set; }
    
    public DbSet<Document> Documents { get; set; }
    
    public DbSet<Program> Programs { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<EducationLevel>  EducationLevels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new ApplicantConfiguration());
        modelBuilder.ApplyConfiguration(new ManagerConfiguration());
        
        modelBuilder.ApplyConfiguration(new AdmissionConfiguration());
        modelBuilder.ApplyConfiguration(new AdmissionProgramConfiguration());
        
        modelBuilder.ApplyConfiguration(new DocumentConfiguration());
        
        modelBuilder.ApplyConfiguration(new ProgramConfiguration());
        modelBuilder.ApplyConfiguration(new FacultyConfiguration());
        modelBuilder.ApplyConfiguration(new EducationLevelConfiguration());
    }
}