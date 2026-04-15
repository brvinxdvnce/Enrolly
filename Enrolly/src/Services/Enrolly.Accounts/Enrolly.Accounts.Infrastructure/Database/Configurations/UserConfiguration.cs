using Enrolly.Accounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enrolly.Accounts.Infrastructure.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasOne(u => u.ApplicantProfile)
            .WithOne(a => a.Account)
            .HasForeignKey<Applicant>(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.ManagerProfile)
            .WithOne(a => a.Account)
            .HasForeignKey<Manager>(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}