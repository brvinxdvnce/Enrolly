using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events.Events.Dictionary.EducationLevelEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Admissions.Infrastructure.Consumers.Dictionary.EducationLevel;

public class EducationLevelUpdatedConsumer : IConsumer<EducationLevelUpdatedEvent>
{
    private readonly IEducationLevelRepository _educationLevelRepository;
    private readonly ILogger<EducationLevelUpdatedConsumer> _logger;
    
    public EducationLevelUpdatedConsumer(IEducationLevelRepository educationLevelRepository, ILogger<EducationLevelUpdatedConsumer> logger)
    {
        _educationLevelRepository = educationLevelRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<EducationLevelUpdatedEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {eduLevelId}", 
            nameof(EducationLevelUpdatedEvent),
            context.Message.Id);

        var educationLevel = new Domain.Entities.EducationLevel()
        {
            Id = context.Message.Id,
            Name = context.Message.Name,
        };
        
        var result = await _educationLevelRepository.Update(educationLevel);
        if (!result.IsSuccess) 
            _logger.LogError("Failed to update EducationLevel {EduLevelId}: {Error}",
                context.Message.Id, result.Error);
        else 
            _logger.LogInformation("Successfully updated EducationLevel {EduLevelId}", educationLevel.Id);
    }
}