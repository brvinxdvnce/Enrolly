using Enrolly.EduDictoinary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enrolly.EduDictionary.Application.Database.Configurations;

public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
{
    public void Configure(EntityTypeBuilder<Faculty> builder)
    {
        builder.ToTable("faculty");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.RelevanceStatus)
            .HasConversion<string>();
    }
}