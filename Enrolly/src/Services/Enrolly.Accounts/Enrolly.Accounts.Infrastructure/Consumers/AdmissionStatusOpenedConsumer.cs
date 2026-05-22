using Enrolly.Accounts.Domain.Repositories;
using Enrolly.Contracts.Events.Events.Admissions;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Accounts.Infrastructure.Consumers;

public class AdmissionStatusOpenedConsumer : IConsumer<AdmissionStatusOpenedEvent>
{
    private readonly ILogger<AdmissionStatusOpenedConsumer> _logger;
    private readonly IApplicantRepository _applicantRepository;
    
    public AdmissionStatusOpenedConsumer(ILogger<AdmissionStatusOpenedConsumer> logger, IApplicantRepository applicantRepository)
    {
        _logger = logger;
        _applicantRepository = applicantRepository;
    }

    public async Task Consume(ConsumeContext<AdmissionStatusOpenedEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Applicant Id: {Applicant}", 
            nameof(AdmissionStatusOpenedEvent),
            context.Message.ApplicantId);

        await _applicantRepository.SetAdmissionStatus(context.Message.ApplicantId, true);
    }
}