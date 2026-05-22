using Enrolly.Documents.Domain.Entities;
using Enrolly.Documents.Infrastructure.Database.Configurations;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using File = Enrolly.Documents.Domain.Entities.File;

namespace Enrolly.Documents.Infrastructure.Database;

public class DocumentsDbContext(DbContextOptions<DocumentsDbContext> options)
    : DbContext(options)
{
    public DbSet<Applicant> Applicants { get; set; }
    public DbSet<Manager> Managers { get; set; }
    
    public DbSet<EducationDocument> Diplomas { get; set; }
    public DbSet<Passport> Passports { get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<EducationDocumentType> EducationDocumentTypes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EducationDocumentTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ApplicantConfiguration());
        modelBuilder.ApplyConfiguration(new PassportConfiguration());
        modelBuilder.ApplyConfiguration(new EducationDocumentConfiguration());
        modelBuilder.ApplyConfiguration(new FileConfiguration());
        modelBuilder.ApplyConfiguration(new ManagerConfiguration());
        
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
        
        base.OnModelCreating(modelBuilder);
    }
}