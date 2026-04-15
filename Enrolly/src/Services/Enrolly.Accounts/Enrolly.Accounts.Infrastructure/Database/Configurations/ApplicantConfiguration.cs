using Enrolly.Accounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enrolly.Accounts.Infrastructure.Database.Configurations;

public class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasOne(a => a.Account)
            .WithOne(u => u.ApplicantProfile)
            .HasForeignKey<Applicant>(a => a.Id);
    }
}