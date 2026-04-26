using Enrolly.Admissions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enrolly.Admissions.Infrastructure.Database.Configurations;

public class AdmissionProgramConfiguration : IEntityTypeConfiguration<AdmissionProgram>
{
    public void Configure(EntityTypeBuilder<AdmissionProgram> builder)
    {
        builder.ToTable("admission_program");

        builder.HasKey(ap => new { ap.ProgramId, ap.AdmissionId });

        builder.Property(ap => ap.AdmissionId)
            .ValueGeneratedNever();
        
        builder.Property(ap => ap.ProgramId)
            .ValueGeneratedNever();

    }
}