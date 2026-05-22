using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events.Events.Dictionary.FacultyEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Admissions.Infrastructure.Consumers.Dictionary.Faculty;

public class FacultyUpdatedConsumer : IConsumer<FacultyUpdatedEvent>
{
    private readonly IFacultyRepository _facultyRepository;
    private readonly ILogger<FacultyUpdatedConsumer> _logger;
    
    public FacultyUpdatedConsumer(IFacultyRepository facultyRepository, ILogger<FacultyUpdatedConsumer> logger)
    {
        _facultyRepository = facultyRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<FacultyUpdatedEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {eduLevelId}, Entity Name: {name}", 
            nameof(FacultyUpdatedEvent),
            context.Message.Id,
            context.Message.Name);

        var faculty = new Domain.Entities.Faculty()
        {
            Id = context.Message.Id,
            CreateTime = context.Message.CreatedAt,
            Name = context.Message.Name,
        };

        var result = await _facultyRepository.Update(faculty);
        
        if (result.IsFailure)
            _logger.LogError("Failed to update Faculty {FacultyId}: {Error}",
                context.Message.Id, result.Error);
        else 
            _logger.LogInformation("Successfully updated Faculty {FacultyId}", faculty.Id);
    }
}