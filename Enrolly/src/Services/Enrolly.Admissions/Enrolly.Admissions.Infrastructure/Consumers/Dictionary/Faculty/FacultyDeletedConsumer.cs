using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events.Events.Dictionary.FacultyEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Admissions.Infrastructure.Consumers.Dictionary.Faculty;

public class FacultyDeletedConsumer : IConsumer<FacultyDeletedEvent>
{
    private readonly IFacultyRepository _facultyRepository;
    private readonly ILogger<FacultyDeletedConsumer> _logger;
    
    public FacultyDeletedConsumer(IFacultyRepository facultyRepository, ILogger<FacultyDeletedConsumer> logger)
    {
        _facultyRepository = facultyRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<FacultyDeletedEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {eduLevelId}", 
            nameof(FacultyDeletedEvent),
            context.Message.Id);
        
        var result = await _facultyRepository.DeleteById(context.Message.Id);
        
        if (result.IsFailure)
            _logger.LogError("Failed to delete Faculty {FacultyId}: {Error}",
                context.Message.Id, result.Error);
        else 
            _logger.LogInformation("Successfully deleted Faculty {FacultyId}", context.Message.Id);
    }
}