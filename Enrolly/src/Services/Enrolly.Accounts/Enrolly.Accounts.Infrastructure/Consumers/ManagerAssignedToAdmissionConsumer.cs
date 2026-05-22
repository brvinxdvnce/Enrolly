using Enrolly.Accounts.Domain.Repositories;
using Enrolly.Contracts.Events.Events.Admissions;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Accounts.Infrastructure.Consumers;

public class ManagerAssignedToAdmissionConsumer : IConsumer<ManagerAssignedToAdmissionEvent>
{
    private readonly ILogger<ManagerAssignedToAdmissionConsumer> _logger;
    private readonly IApplicantRepository _applicantRepository;

    public ManagerAssignedToAdmissionConsumer(
        ILogger<ManagerAssignedToAdmissionConsumer> logger, 
        IApplicantRepository applicantRepository)
    {
        _logger = logger;
        _applicantRepository = applicantRepository;
    }

    public async Task Consume(ConsumeContext<ManagerAssignedToAdmissionEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Applicant Id: {Applicant}", 
            nameof(ManagerAssignedToAdmissionEvent),
            context.Message.ApplicantId);
        
        var added = await _applicantRepository.AddManagerToApplicantAsync(
                context.Message.ApplicantId,
                context.Message.ManagerId);
        
        if (added)
            _logger.LogInformation("Manager {ManagerId} successfully added to Applicant {ApplicantId}",
                context.Message.ManagerId, context.Message.ApplicantId);
        else
            _logger.LogWarning("Could not add manager {ManagerId} to applicant {ApplicantId}: applicant or manager not found",
                context.Message.ManagerId, context.Message.ApplicantId);
    }
}