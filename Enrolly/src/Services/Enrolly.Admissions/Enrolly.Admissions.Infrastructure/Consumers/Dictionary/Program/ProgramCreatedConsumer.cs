using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events.Events.Dictionary.ProgramEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Admissions.Infrastructure.Consumers.Dictionary.Program;

public class ProgramCreatedConsumer : IConsumer<ProgramImportedEvent>
{
    private readonly IProgramRepository _programRepository;
    private readonly ILogger<ProgramCreatedConsumer> _logger;
    
    public ProgramCreatedConsumer(IProgramRepository programRepository, ILogger<ProgramCreatedConsumer> logger)
    {
        _programRepository = programRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ProgramImportedEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {eduLevelId}, Entity Name: {name}", 
            nameof(ProgramImportedEvent),
            context.Message.Id,
            context.Message.Name);

        
        var program = new Domain.Entities.Program()
        {
            Id = context.Message.Id,
            Name = context.Message.Name,
            Code = context.Message.Code,
            CreateTime = context.Message.CreatedAt,
            EducationForm = context.Message.EducationForm,
            FacultyId = context.Message.FacultyId,
            EducationLevelId = context.Message.EducationLevelId,
        };

        var result = await _programRepository.Add(program);
        
        if (result.IsFailure)
            _logger.LogError("Failed to add Program {ProgramId}: {Error}",
                context.Message.Id, result.Error);
        else 
            _logger.LogInformation("Successfully added Program {ProgramId}", program.Id);
    }
}