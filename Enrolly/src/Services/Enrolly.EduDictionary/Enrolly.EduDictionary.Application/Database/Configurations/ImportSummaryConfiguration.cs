using Enrolly.EduDictionary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enrolly.EduDictionary.Application.Database.Configurations;

public class ImportSummaryConfiguration : IEntityTypeConfiguration<ImportSummary>
{
    public void Configure(EntityTypeBuilder<ImportSummary> builder)
    {
        builder.ToTable("import_summary");
        
        builder.HasKey(x => x.Id);
        
        
    }
}