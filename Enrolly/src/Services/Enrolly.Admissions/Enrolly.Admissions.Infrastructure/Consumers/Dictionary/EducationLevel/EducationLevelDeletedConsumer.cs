using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events.Events.Dictionary.EducationLevelEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Admissions.Infrastructure.Consumers.Dictionary.EducationLevel;

public class EducationLevelDeletedConsumer : IConsumer<EducationLevelDeletedEvent>
{
    private readonly IEducationLevelRepository _educationLevelRepository;
    private readonly ILogger<EducationLevelDeletedConsumer> _logger;
        
    public EducationLevelDeletedConsumer(IEducationLevelRepository educationLevelRepository, ILogger<EducationLevelDeletedConsumer> logger)
    {
        _educationLevelRepository = educationLevelRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<EducationLevelDeletedEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {eduLevelId}", 
            nameof(EducationLevelDeletedEvent),
            context.Message.Id);
        
        var result = await _educationLevelRepository.DeleteById(context.Message.Id);
        
        if (result.IsFailure)
            _logger.LogError("Failed to delete EducationLevel {EduLevelId}: {Error}",
                context.Message.Id, result.Error);
        else 
            _logger.LogInformation("Successfully deleted EducationLevel {EduLevelId}", context.Message.Id);
    }
}