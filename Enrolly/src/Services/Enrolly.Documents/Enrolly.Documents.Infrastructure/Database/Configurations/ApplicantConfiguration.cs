using Enrolly.Documents.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enrolly.Documents.Infrastructure.Database.Configurations;

public class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder.HasKey(a => a.Id);
    
        builder.Property(a => a.Id)
            .ValueGeneratedNever();

        builder.HasOne(a => a.Passport)
            .WithOne(p => p.Applicant)
            .HasForeignKey<Passport>(p => p.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.Diplomas)
            .WithOne(d => d.Applicant)
            .HasForeignKey(d => d.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}