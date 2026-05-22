using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Admissions.Domain.Entities;
using Enrolly.Contracts.Events.Events.Dictionary.EducationLevelEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Admissions.Infrastructure.Consumers.Dictionary.EducationLevel;

public class EducationLevelCreatedConsumer : IConsumer<EducationLevelImportedEvent>
{
    private readonly IEducationLevelRepository _educationLevelRepository;
    private readonly ILogger<EducationLevelCreatedConsumer> _logger;
    
    public EducationLevelCreatedConsumer(IEducationLevelRepository educationLevelRepository, ILogger<EducationLevelCreatedConsumer> logger)
    {
        _educationLevelRepository = educationLevelRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<EducationLevelImportedEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {eduLevelId}, Entity Name: {name}", 
            nameof(EducationLevelImportedEvent),
            context.Message.Id,
            context.Message.Name);
        
        var educationLevel = new Domain.Entities.EducationLevel() {
            Id = context.Message.Id, 
            Name = context.Message.Name
        };

        var result = await _educationLevelRepository.Add(educationLevel);
        
        if (result.IsFailure)
            _logger.LogError("Failed to add EducationLevel {EduLevelId}: {Error}",
                context.Message.Id, result.Error);
        else 
            _logger.LogInformation("Successfully added EducationLevel {EduLevelId}", educationLevel.Id);
    }
}