using Enrolly.Accounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enrolly.Accounts.Infrastructure.Database.Configurations;

public class CitizenshipConfiguration : IEntityTypeConfiguration<Citizenship>
{
    public void Configure(EntityTypeBuilder<Citizenship> builder)
    {
        builder.ToTable("citizenship");
        
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();
    }
}