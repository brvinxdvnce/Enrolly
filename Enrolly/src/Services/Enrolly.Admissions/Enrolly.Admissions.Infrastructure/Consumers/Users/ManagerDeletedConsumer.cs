using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Admissions.Infrastructure.Consumers.Users;

public class ManagerDeletedConsumer : IConsumer<ManagerDeletedEvent>
{
    private readonly IManagerRepository _managerRepository;
    private readonly ILogger<ManagerDeletedConsumer> _logger;
    
    public ManagerDeletedConsumer(ILogger<ManagerDeletedConsumer> logger, IManagerRepository managerRepository)
    {
        _logger = logger;
        _managerRepository = managerRepository;
    }

    public async Task Consume(ConsumeContext<ManagerDeletedEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {ApplicantId}",
            nameof(ManagerDeletedEvent),
            context.Message.ManagerId);
        
        var result = await _managerRepository.DeleteById(context.Message.ManagerId);
        
        if (result.IsFailure)
            _logger.LogError("Failed to delete manager with Id {ManagerId}: {Error}",
                context.Message.ManagerId, result.Error);
        else 
            _logger.LogInformation("Successfully deleted manager with Id {ManagerId}", context.Message.ManagerId);

    }
}