using Enrolly.Contracts.Events.Events.Staff;
using Enrolly.Documents.Domain.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Documents.Infrastructure.Consumers.Users;

public class ManagerGradeChangedConsumer : IConsumer<ManagerGradeChangedEvent>
{
    private readonly IManagerRepository _managerRepository;
    private readonly ILogger<ManagerGradeChangedConsumer> _logger;

    public ManagerGradeChangedConsumer(IManagerRepository repository, ILogger<ManagerGradeChangedConsumer> logger)
    {
        _managerRepository = repository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ManagerGradeChangedEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {ManagerId}", 
            nameof(ManagerGradeChangedEvent),
            context.Message.Id);

        var result = await _managerRepository.ChangeGrade(context.Message.Id, context.Message.NewGrade);
        
        if (result.IsFailure)
            _logger.LogError("Failed to change grade of Manager with Id {ManagerId}: {Error}",
                context.Message.Id, result.Error);
        else 
            _logger.LogInformation("Successfully changed grade of Manager with Id {ManagerId}", context.Message.Id);
    }
}