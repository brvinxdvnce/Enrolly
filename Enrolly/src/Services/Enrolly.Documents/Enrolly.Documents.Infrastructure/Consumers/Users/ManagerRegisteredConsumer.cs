using Enrolly.Contracts.Events.Events;
using Enrolly.Documents.Domain.Entities;
using Enrolly.Documents.Domain.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Documents.Infrastructure.Consumers.Users;

public class ManagerRegisteredConsumer : IConsumer<ManagerRegisteredEvent>
{
    private readonly IManagerRepository _managerRepository;
    private readonly ILogger<ManagerRegisteredConsumer> _logger;

    public ManagerRegisteredConsumer(IManagerRepository managerRepository, ILogger<ManagerRegisteredConsumer> logger)
    {
        _managerRepository = managerRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ManagerRegisteredEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {ManagerId}", 
            nameof(ManagerRegisteredEvent),
            context.Message.ManagerId);

        var manager = new Manager()
        {
            Id = context.Message.ManagerId,
            Name = context.Message.ManagerName,
            Email = context.Message.ManagerEmail
        };

        var result = await _managerRepository.Add(manager);
        
        if (result.IsFailure)
            _logger.LogError("Failed to add Manager with Id {ManagerId}: {Error}",
                context.Message.ManagerId, result.Error);
        else 
            _logger.LogInformation("Successfully added Manager with Id {ManagerId}", context.Message.ManagerId);
    }
}