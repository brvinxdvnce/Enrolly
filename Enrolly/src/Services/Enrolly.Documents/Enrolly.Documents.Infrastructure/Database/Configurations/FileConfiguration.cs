using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = Enrolly.Documents.Domain.Entities.File;

namespace Enrolly.Documents.Infrastructure.Database.Configurations;

public class FileConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.ToTable("file");
        
        builder.HasKey(x => x.Id);
    }
}