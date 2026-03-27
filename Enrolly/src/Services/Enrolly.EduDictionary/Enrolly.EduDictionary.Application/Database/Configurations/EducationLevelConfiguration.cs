using Enrolly.EduDictionary.Domain.Entities;
using Enrolly.EduDictoinary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enrolly.EduDictionary.Application.Database.Configurations;

public class EducationLevelConfiguration : IEntityTypeConfiguration<EducationLevel>
{
    public void Configure(EntityTypeBuilder<EducationLevel> builder)
    {
        builder.ToTable("education_level");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.RelevanceStatus)
            .HasConversion<string>();
    }
}