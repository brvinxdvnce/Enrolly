using Enrolly.Documents.Domain.Entities;
using Enrolly.Documents.Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using File = Enrolly.Documents.Domain.Entities.File;

namespace Enrolly.Documents.Infrastructure.Database;

public class DocumentsDbContext(DbContextOptions<DocumentsDbContext> options)
    : DbContext(options)
{
    public DbSet<Applicant> Applicants { get; set; }
    public DbSet<EducationDocument> Diplomas { get; set; }
    public DbSet<Passport> Passports { get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<EducationDocumentType> EducationDocumentTypes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EducationDocumentTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ApplicantConfiguration());
        modelBuilder.ApplyConfiguration(new PassportConfiguration());
        modelBuilder.ApplyConfiguration(new DiplomaConfiguration());
        modelBuilder.ApplyConfiguration(new FileConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}