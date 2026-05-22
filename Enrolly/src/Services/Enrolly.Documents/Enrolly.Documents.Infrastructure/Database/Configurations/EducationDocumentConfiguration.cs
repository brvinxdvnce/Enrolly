using Enrolly.Documents.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enrolly.Documents.Infrastructure.Database.Configurations;

public class EducationDocumentConfiguration : IEntityTypeConfiguration<EducationDocument>
{
    public void Configure(EntityTypeBuilder<EducationDocument> builder)
    {
        builder.ToTable("education_document_meta");
        
        builder.HasKey(a => a.Id);
        
        builder.HasMany(e => e.Files)
            .WithOne(e => e.EducationDocument)
            .HasForeignKey(e => e.EducationDocumentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.DocumentType)
            .WithMany();
    }
}