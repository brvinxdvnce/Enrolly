using System.Text.Json;
using Enrolly.Accounts.Domain.Entities;
using Enrolly.Accounts.Infrastructure.Database.Configurations;
using Enrolly.Contracts.Events.Abstractions;
using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
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
        
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
        
        //SeedAdmin(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entitiesWithEvents = ChangeTracker.Entries<DomainEntity>()
            .Select(e => e.Entity)
            .Where(e => e.Events.Any())
            .ToList();
        
        if (entitiesWithEvents.Any())
        {
            var outboxState = new OutboxState
            {
                OutboxId = Guid.NewGuid(),
                LockId = Guid.NewGuid(),
                Created = DateTime.UtcNow
            };
            await Set<OutboxState>().AddAsync(outboxState, cancellationToken);
            
            var newOutboxMessages =
                entitiesWithEvents
                    .SelectMany(e => e.Events)
                    .Select(e => new OutboxMessage()
                    {
                        MessageId = Guid.NewGuid(),
                        MessageType = e.GetType().FullName,
                        ContentType = "application/json",
                        Body = JsonSerializer.Serialize(e, e.GetType()),
                        SentTime = default(DateTime),
                        EnqueueTime = null,
                        OutboxId = outboxState.OutboxId,
                        
                    })
                    .ToList();

            if (newOutboxMessages.Any())
            {
                await Set<OutboxMessage>().AddRangeAsync(newOutboxMessages, cancellationToken);
            }
        }

        int result = await base.SaveChangesAsync(cancellationToken);

        foreach (var entity in entitiesWithEvents)
            entity.ClearEvents();
        
        return result;
    }

    /*private void SeedAdmin(ModelBuilder modelBuilder)
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
    }*/
}
