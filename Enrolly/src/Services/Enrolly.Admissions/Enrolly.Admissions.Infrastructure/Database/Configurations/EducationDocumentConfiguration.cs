using Enrolly.Admissions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enrolly.Admissions.Infrastructure.Database.Configurations;

public class EducationDocumentConfiguration : IEntityTypeConfiguration<EducationDocument>
{
    public void Configure(EntityTypeBuilder<EducationDocument> builder)
    {
        builder.ToTable("education_document");
        
        builder.HasKey(x => x.DocumentId);
        
        builder.Property(x => x.DocumentId)
            .ValueGeneratedNever();
    }
}