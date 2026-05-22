using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Admissions.Infrastructure.Consumers.Users;

public class ApplicantDeletedConsumer : IConsumer<ApplicantDeletedEvent>
{
    private readonly IApplicantRepository _applicantRepository;
    private readonly ILogger<ApplicantDeletedConsumer> _logger;
    
    public ApplicantDeletedConsumer(IApplicantRepository applicantRepository, ILogger<ApplicantDeletedConsumer> logger)
    {
        _applicantRepository = applicantRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ApplicantDeletedEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {ApplicantId}",
            nameof(ApplicantDeletedEvent),
            context.Message.Id);
        
        var result = await _applicantRepository.DeleteById(context.Message.Id);
        
        if (result.IsFailure)
            _logger.LogError("Failed to delete applicant {applicantId}: {Error}",
                context.Message.Id, result.Error);
        else 
            _logger.LogInformation("Successfully deleted applicant {ApplicantId}", context.Message.Id);
    }
}