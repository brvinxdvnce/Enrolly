using Enrolly.EduDictoinary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enrolly.EduDictionary.Application.Database.Configurations;

public class ProgramConfiguration : IEntityTypeConfiguration<Program>
{
    public void Configure(EntityTypeBuilder<Program> builder)
    {
        builder.ToTable("program");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.RelevanceStatus)
            .HasConversion<string>();
        
        builder.HasOne(x => x.Faculty)
            .WithMany()
            .HasForeignKey(x => x.FacultyId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.EducationLevel)
            .WithMany()
            .HasForeignKey(x => x.EducationLevelId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
    }
}