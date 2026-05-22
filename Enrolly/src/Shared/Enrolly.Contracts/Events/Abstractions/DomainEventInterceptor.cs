using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Enrolly.Contracts.Events.Abstractions;

public class DomainEventInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken ct = default)
    {
        if (eventData.Context is not null)
            await PublishDomainEventsAsync(eventData.Context, ct);

        return await base.SavingChangesAsync(eventData, result, ct);
    }

    private static async Task PublishDomainEventsAsync(DbContext context, CancellationToken ct)
    {
        var logger = context.GetService<ILogger<DomainEventInterceptor>>();
        logger.LogInformation("Publishing domain events");
        
        var entitiesWithEvents = context.ChangeTracker
            .Entries<DomainEntity>()
            .Select(e => e.Entity)
            .Where(e => e.Events.Any())
            .ToList();

        if (!entitiesWithEvents.Any()) return;

        var publishEndpoint = context.GetService<IPublishEndpoint>();
        if (publishEndpoint is null) return;

        foreach (var entity in entitiesWithEvents)
        {
            logger.LogInformation("Events of current entity : {events}", entity.Events);
            
            foreach (var domainEvent in entity.Events)
                await publishEndpoint.Publish(domainEvent, domainEvent.GetType(), ct);
            
            entity.ClearEvents();
        }
    }
}