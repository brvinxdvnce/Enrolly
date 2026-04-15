using Enrolly.Documents.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enrolly.Documents.Infrastructure.Database.Configurations;

public class PassportConfiguration : IEntityTypeConfiguration<Passport>
{
    public void Configure(EntityTypeBuilder<Passport> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .ValueGeneratedNever();
        
        builder.HasMany(p => p.Files)
            .WithOne(p => p.Passport)
            .HasForeignKey(p => p.PassportId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}