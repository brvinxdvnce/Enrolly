using Enrolly.Accounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enrolly.Accounts.Infrastructure.Database.Configurations;

public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
{
    public void Configure(EntityTypeBuilder<Manager> builder)
    {
        builder.HasKey(m => m.Id);
        
        builder.HasOne(a => a.Account)
            .WithOne(u => u.ManagerProfile)
            .HasForeignKey<Manager>(m => m.Id);

        builder.Property(m => m.Grade)
            .HasConversion<string>();
    }
}