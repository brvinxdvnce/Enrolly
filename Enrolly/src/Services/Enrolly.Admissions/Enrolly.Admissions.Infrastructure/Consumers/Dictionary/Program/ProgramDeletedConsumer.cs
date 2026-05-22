using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events.Events.Dictionary.ProgramEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Admissions.Infrastructure.Consumers.Dictionary.Program;

public class ProgramDeletedConsumer : IConsumer<ProgramDeletedEvent>
{
    private readonly IProgramRepository _programRepository;
    private readonly ILogger<ProgramDeletedConsumer> _logger;

    public ProgramDeletedConsumer(IProgramRepository programRepository, ILogger<ProgramDeletedConsumer> logger)
    {
        _programRepository = programRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ProgramDeletedEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {eduLevelId}", 
            nameof(ProgramDeletedEvent),
            context.Message.Id);

        var result = await _programRepository.DeleteById(context.Message.Id);
        
        if (result.IsFailure)
            _logger.LogError("Failed to delete Program {ProgramId}: {Error}",
                context.Message.Id, result.Error);
        else 
            _logger.LogInformation("Successfully deleted Program {ProgramId}", context.Message.Id);
    }
}