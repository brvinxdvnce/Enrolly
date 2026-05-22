using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Enrolly.Shared.Logging.Utils.Database;

public class NpgsqlDbContextFactory<TContext> 
    : IDesignTimeDbContextFactory<TContext> 
    where TContext : DbContext
{
    public TContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<TContext>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DbConnection"));

        return Activator.CreateInstance(typeof(TContext), optionsBuilder.Options) as TContext
               ?? throw new InvalidOperationException($"Could not create instance of {typeof(TContext).Name}");
    }
}