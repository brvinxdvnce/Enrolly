using Enrolly.Contracts.Events;
using Enrolly.Notifications.Models.Notifications;
using Enrolly.Notifications.Services.Interfaces;
using MassTransit;

namespace Enrolly.Notifications.Consumers;

public class AdmissionStatusChangedConsumer : IConsumer<AdmissionStatusChangedEvent>
{
    private readonly IMailService _mailService;
    private readonly ILogger<AdmissionStatusChangedConsumer> _logger;

    public AdmissionStatusChangedConsumer(IMailService mailService, ILogger<AdmissionStatusChangedConsumer> logger)
    {
        _mailService = mailService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<AdmissionStatusChangedEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {ApplicantId}",
            nameof(AdmissionStatusChangedEvent),
            context.Message.AdmissionId);
        
        await _mailService
            .SendAsync(
                new AdmissionStatusChangedNotification(
                    context.Message.ApplicantEmail,
                    context.Message.ApplicantName,
                    context.Message.NewStatus));
        
        _logger.LogInformation("Successfully sent message to {email}", context.Message.ApplicantEmail);
    }
}
