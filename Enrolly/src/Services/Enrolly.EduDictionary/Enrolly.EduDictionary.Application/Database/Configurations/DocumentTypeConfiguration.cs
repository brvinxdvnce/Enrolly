using Enrolly.EduDictionary.Domain.Entities;
using Enrolly.EduDictionary.Domain.Enums;
using Enrolly.EduDictoinary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enrolly.EduDictionary.Application.Database.Configurations;

public class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
{
    public void Configure(EntityTypeBuilder<DocumentType> builder)
    {
        builder.ToTable("document_type");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.HasOne(x => x.EducationLevel)
            .WithMany()
            .HasForeignKey(x => x.EducationLevelId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasMany(x => x.NextEducationLevels)
            .WithMany()
            .UsingEntity(i => i.ToTable("document_type_next_edu_level"));

        builder.Property(x => x.RelevanceStatus)
            .HasConversion<string>();
    }
}