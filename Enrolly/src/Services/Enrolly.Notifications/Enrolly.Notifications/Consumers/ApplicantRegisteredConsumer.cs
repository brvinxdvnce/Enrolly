using Enrolly.Contracts.Events;
using Enrolly.Contracts.Events.Events;
using Enrolly.Notifications.Models.Notifications;
using Enrolly.Notifications.Services.Implementations;
using Enrolly.Notifications.Services.Interfaces;
using MassTransit;

namespace Enrolly.Notifications.Consumers;

public class ApplicantRegisteredConsumer : IConsumer<ApplicantRegisteredEvent>
{
    private readonly ILogger<ApplicantRegisteredConsumer> _logger;
    private readonly IMailService  _emailService;
    
    public ApplicantRegisteredConsumer(ILogger<ApplicantRegisteredConsumer> logger, IMailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    public async Task Consume(ConsumeContext<ApplicantRegisteredEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {ApplicantId}",
            nameof(ApplicantRegisteredEvent),
            context.Message.ApplicantId);
        
        await _emailService
            .SendAsync(
                SubAccountCreatedNotification
                    .ToApplicant(
                        context.Message.Email,
                        context.Message.ApplicantName));
        
        _logger.LogInformation("Successfully sent message to {email}", context.Message.Email);
    }
}