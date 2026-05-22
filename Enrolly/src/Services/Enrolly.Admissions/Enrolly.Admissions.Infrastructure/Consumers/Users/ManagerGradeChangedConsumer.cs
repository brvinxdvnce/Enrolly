using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events.Events.Staff;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Admissions.Infrastructure.Consumers.Users;

public class ManagerGradeChangedConsumer : IConsumer<ManagerGradeChangedEvent>
{
    private readonly ILogger<ManagerGradeChangedConsumer> _logger;
    private readonly IManagerRepository _managerRepository;
    
    public ManagerGradeChangedConsumer(ILogger<ManagerGradeChangedConsumer> logger, IManagerRepository managerRepository)
    {
        _logger = logger;
        _managerRepository = managerRepository;
    }

    public async Task Consume(ConsumeContext<ManagerGradeChangedEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {ApplicantId}",
            nameof(ManagerGradeChangedEvent),
            context.Message.Id);

        var result = await _managerRepository.ChangeGrade(context.Message.Id, context.Message.NewGrade);
        
        if (result.IsFailure)
            _logger.LogError("Failed to change grade of manager with Id {ManagerId}: {Error}",
                context.Message.Id, result.Error);
        else 
            _logger.LogInformation("Successfully changed grade of manager with Id {ManagerId}", context.Message.Id);
    }
}