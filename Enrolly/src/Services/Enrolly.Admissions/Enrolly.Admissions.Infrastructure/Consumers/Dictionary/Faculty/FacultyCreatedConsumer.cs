using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events.Events.Dictionary.FacultyEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Admissions.Infrastructure.Consumers.Dictionary.Faculty;

public class FacultyCreatedConsumer : IConsumer<FacultyImportedEvent>
{
    private readonly IFacultyRepository _facultyRepository;
    private readonly ILogger<FacultyCreatedConsumer> _logger;
    
    public FacultyCreatedConsumer(IFacultyRepository facultyRepository, ILogger<FacultyCreatedConsumer> logger)
    {
        _facultyRepository = facultyRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<FacultyImportedEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {eduLevelId}, Entity Name: {name}", 
            nameof(FacultyImportedEvent),
            context.Message.Id,
            context.Message.Name);

        var faculty = new Domain.Entities.Faculty()
        {
            Id = context.Message.Id,
            CreateTime = context.Message.CreatedAt,
            Name = context.Message.Name,
        };

        var result = await _facultyRepository.Add(faculty);
        
        if (result.IsFailure)
            _logger.LogError("Failed to add Faculty {FacultyId}: {Error}",
                context.Message.Id, result.Error);
        else 
            _logger.LogInformation("Successfully added Faculty {FacultyId}", faculty.Id);
    }
}