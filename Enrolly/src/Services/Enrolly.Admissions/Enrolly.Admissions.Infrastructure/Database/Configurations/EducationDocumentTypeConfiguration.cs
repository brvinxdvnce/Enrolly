using Enrolly.Admissions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enrolly.Admissions.Infrastructure.Database.Configurations;

public class EducationDocumentTypeConfiguration : IEntityTypeConfiguration<EducationDocumentType>
{
    public void Configure(EntityTypeBuilder<EducationDocumentType> builder)
    {
        builder.ToTable("education_document_type");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
    }
}