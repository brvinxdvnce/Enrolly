using Enrolly.Accounts.Domain.Entities;
using Enrolly.Accounts.Infrastructure.Database.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Accounts.Infrastructure.Database;

public class UsersDbContext(DbContextOptions<UsersDbContext> options) :
    IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Applicant> Applicants { get; set; }
    public DbSet<Manager> Managers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ApplicantConfiguration());
        modelBuilder.ApplyConfiguration(new ManagerConfiguration());
        
        SeedAdmin(modelBuilder);
    }

    private void SeedAdmin(ModelBuilder modelBuilder)
    {
        var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var adminRoleId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        
        modelBuilder.Entity<IdentityRole<Guid>>().HasData(new IdentityRole<Guid>
        {
            Id = adminRoleId,
            Name = "Admin",
            NormalizedName = "ADMIN"
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
            ConcurrencyStamp = "0f8fad5b-d9cb-469f-a165-70867728950e"
        };
        
        var password = new PasswordHasher<User>();
        var hashed = password.HashPassword(admin, "admin");
        admin.PasswordHash = hashed;
        
        modelBuilder.Entity<User>().HasData(admin);
        
        modelBuilder.Entity<IdentityUserRole<Guid>>()
            .HasData(new IdentityUserRole<Guid>
            {
                RoleId = adminRoleId, 
                UserId = adminId
            });
    }
}
