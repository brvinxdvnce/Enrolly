using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Admissions.Infrastructure.Consumers.Users;

public class ManagerRegisteredConsumer : IConsumer<ManagerRegisteredEvent>
{
    private readonly IManagerRepository _managerRepository;
    private readonly ILogger<ManagerRegisteredConsumer> _logger;

    public ManagerRegisteredConsumer(ILogger<ManagerRegisteredConsumer> logger, IManagerRepository managerRepository)
    {
        _logger = logger;
        _managerRepository = managerRepository;
    }

    public async Task Consume(ConsumeContext<ManagerRegisteredEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {ApplicantId}",
            nameof(ApplicantRegisteredEvent),
            context.Message.ManagerId);

        var newManager = new Manager()
        {
            Id = context.Message.ManagerId,
            Email = context.Message.ManagerEmail,
            Name = context.Message.ManagerName
        };
        
        var result = await _managerRepository.Add(newManager);
        
        if (result.IsFailure)
            _logger.LogError("Failed to add manager with Id {ManagerId}: {Error}",
                context.Message.ManagerId, result.Error);
        else 
            _logger.LogInformation("Successfully added manager with Id {ManagerId}", context.Message.ManagerId);
    }
}