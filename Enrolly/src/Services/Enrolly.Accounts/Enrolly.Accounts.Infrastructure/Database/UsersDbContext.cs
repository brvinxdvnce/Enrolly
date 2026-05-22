using Enrolly.Accounts.Domain.Entities;
using Enrolly.Accounts.Infrastructure.Database.Configurations;
using Enrolly.Accounts.Infrastructure.Database.Seeders;
using Enrolly.Contracts.Events.Abstractions;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Accounts.Infrastructure.Database;


public class UsersDbContext(DbContextOptions<UsersDbContext> options) :
    IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    
    public DbSet<Applicant> Applicants { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<Citizenship> Citizenships { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ApplicantConfiguration());
        modelBuilder.ApplyConfiguration(new ManagerConfiguration());
        modelBuilder.ApplyConfiguration(new CitizenshipConfiguration());
        
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();

        modelBuilder.SeedCitizenships();
        modelBuilder.SeedAdmins();
    }
}