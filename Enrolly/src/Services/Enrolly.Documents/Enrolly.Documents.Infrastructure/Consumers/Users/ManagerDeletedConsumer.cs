using Enrolly.Contracts.Events.Events;
using Enrolly.Documents.Domain.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Documents.Infrastructure.Consumers.Users;

public class ManagerDeletedConsumer : IConsumer<ManagerDeletedEvent>
{
    private readonly IManagerRepository _managerRepository;
    private readonly ILogger<ManagerDeletedConsumer> _logger;

    public ManagerDeletedConsumer(IManagerRepository managerRepository, ILogger<ManagerDeletedConsumer> logger)
    {
        _managerRepository = managerRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ManagerDeletedEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {ManagerId}", 
            nameof(ManagerDeletedEvent),
            context.Message.ManagerId);

        var result = await _managerRepository.DeleteById(context.Message.ManagerId);
        
        if (result.IsFailure)
            _logger.LogError("Failed to delete Manager with Id {ManagerId}: {Error}",
                context.Message.ManagerId, result.Error);
        else 
            _logger.LogInformation("Successfully deleted Manager with Id {ManagerId}", context.Message.ManagerId);
    }
}