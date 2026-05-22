using Enrolly.Contracts.Events.Abstractions;
using Enrolly.EduDictionary.Application.Database.Configurations;
using Enrolly.EduDictionary.Domain.Entities;
using Enrolly.EduDictoinary.Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.EduDictionary.Application.Database;

public class DictionaryDbContext(DbContextOptions<DictionaryDbContext> options) 
    : DbContext(options)
{
    //public DictionaryDbContext(DbContextOptions<DictionaryDbContext> options) : this(options, null) { }

    public DbSet<ImportSummary> Imports { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<EducationLevel> EducationLevels { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Program> Programs { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new DocumentTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ImportSummaryConfiguration());
        modelBuilder.ApplyConfiguration(new EducationLevelConfiguration());
        modelBuilder.ApplyConfiguration(new FacultyConfiguration());
        modelBuilder.ApplyConfiguration(new ProgramConfiguration());
        
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}