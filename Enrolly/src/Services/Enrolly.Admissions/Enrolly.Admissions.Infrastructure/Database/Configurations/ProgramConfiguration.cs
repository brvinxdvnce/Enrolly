using Enrolly.Admissions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enrolly.Admissions.Infrastructure.Database.Configurations;

public class ProgramConfiguration : IEntityTypeConfiguration<Program>
{
    public void Configure(EntityTypeBuilder<Program> builder)
    {
        builder.ToTable("program");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .ValueGeneratedNever();
        
        builder.HasOne(x => x.Faculty)
            .WithMany()
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.EducationLevel)
            .WithMany()
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}