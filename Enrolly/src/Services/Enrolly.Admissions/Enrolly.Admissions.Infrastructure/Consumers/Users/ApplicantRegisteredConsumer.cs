using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events;
using Enrolly.Contracts.Events.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Admissions.Infrastructure.Consumers.Users;

public class ApplicantRegisteredConsumer : IConsumer<ApplicantRegisteredEvent>
{
    private readonly IApplicantRepository _applicantRepository;
    private readonly ILogger<ApplicantRegisteredConsumer> _logger;
    
    public ApplicantRegisteredConsumer(IApplicantRepository applicantRepository, ILogger<ApplicantRegisteredConsumer> logger)
    {
        _applicantRepository = applicantRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ApplicantRegisteredEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {ApplicantId}",
            nameof(ApplicantRegisteredEvent),
            context.Message.ApplicantId);
        
        var applicant = new Applicant()
        {
            Id = context.Message.ApplicantId,
            Name = context.Message.ApplicantName,
            Email = context.Message.Email
        };
        
        var result = await _applicantRepository.Add(applicant);
        
        if (result.IsFailure)
            _logger.LogError("Failed to add applicant {applicantId}: {Error}",
                context.Message.ApplicantId, result.Error);
        else 
            _logger.LogInformation("Successfully added applicant {ApplicantId}", context.Message.ApplicantId);
    }
}


