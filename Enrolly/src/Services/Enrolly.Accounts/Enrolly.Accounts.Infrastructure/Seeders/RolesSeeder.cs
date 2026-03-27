using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enrolly.Accounts.Infrastructure.Seeders;

public static class RolesSeeder
{
    public static async Task SeedRoles(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var roles = configuration.GetSection("Roles").Get<string[]>() ?? [];
        
        foreach (var role in roles)
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
    }
}