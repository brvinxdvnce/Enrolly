using Enrolly.Accounts.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Accounts.Infrastructure.Database.Seeders;

public static class AdminSeederExtension
{
    public static ModelBuilder SeedAdmins(this ModelBuilder modelBuilder)
    {
        var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var adminRoleId = Guid.Parse("22222222-2222-2222-2222-222222222222");
    
        modelBuilder.Entity<IdentityRole<Guid>>().HasData(new IdentityRole<Guid>
        {
            Id = adminRoleId,
            Name = "Admin",
            NormalizedName = "ADMIN",
            ConcurrencyStamp = "222cf406-7901-45e0-a625-2fd0f8ab2b27"
        });
    
        var admin = new User
        {
            Id = adminId,
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "ivanovdanil.hits2024@gmail.com",
            NormalizedEmail = "IVANOVDANIL.HITS2024@GMAIL.COM",
            EmailConfirmed = true,
            SecurityStamp = "7c9e6679-7425-40de-944b-e07fc1f90ae7",
            ConcurrencyStamp = "0f8fad5b-d9cb-469f-a165-70867728950e",
            PasswordHash = "AQAAAAIAAYagAAAAEK/Rx9lXaMVl2XRpYwE8soOLgg4u8mECM985xTPE+23pqnopjLqH1iyxEVhE0Y8c8A=="
        };
        
        // Пароль: Enrolly-Admin-Password-123
        // Хеш: AQAAAAIAAYagAAAAEK/Rx9lXaMVl2XRpYwE8soOLgg4u8mECM985xTPE+23pqnopjLqH1iyxEVhE0Y8c8A==


        
        modelBuilder.Entity<User>().HasData(admin);
    
        modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
        {
            RoleId = adminRoleId,
            UserId = adminId
        });
     
        return modelBuilder;
    }
}