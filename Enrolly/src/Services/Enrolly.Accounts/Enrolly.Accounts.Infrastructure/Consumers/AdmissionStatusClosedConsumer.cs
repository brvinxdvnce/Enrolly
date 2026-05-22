using Enrolly.Accounts.Domain.Entities;
using Enrolly.Accounts.Domain.Repositories;
using Enrolly.Contracts.Events.Events.Admissions;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Enrolly.Accounts.Infrastructure.Consumers;

public class AdmissionStatusClosedConsumer : IConsumer<AdmissionStatusClosedEvent>
{
    private readonly ILogger<AdmissionStatusClosedConsumer> _logger;
    private readonly IApplicantRepository _applicantRepository;
    
    public AdmissionStatusClosedConsumer(
        ILogger<AdmissionStatusClosedConsumer> logger,
        IApplicantRepository applicantRepository)
    {
        _logger = logger;
        _applicantRepository = applicantRepository;
    }

    public async Task Consume(ConsumeContext<AdmissionStatusClosedEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Applicant Id: {Applicant}",
            nameof(AdmissionStatusClosedEvent),
            context.Message.ApplicantId);

        await _applicantRepository.SetAdmissionStatus(context.Message.ApplicantId, false);
    }
}
