using Enrolly.Contracts.Events.Events;
using Enrolly.Notifications.Models.Notifications;
using Enrolly.Notifications.Services.Interfaces;
using MassTransit;

namespace Enrolly.Notifications.Consumers;

public class ManagerRegisteredConsumer : IConsumer<ManagerRegisteredEvent>
{
    private readonly IMailService _mailService;
    private readonly ILogger<ManagerRegisteredConsumer> _logger;
    
    public ManagerRegisteredConsumer(IMailService mailService, ILogger<ManagerRegisteredConsumer> logger)
    {
        _mailService = mailService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ManagerRegisteredEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {ApplicantId}",
            nameof(ManagerRegisteredEvent),
            context.Message.ManagerId);
        
        await _mailService
            .SendAsync(
                SubAccountCreatedNotification
                    .ToManager(
                        context.Message.ManagerEmail,
                        context.Message.ManagerName));
        
        _logger.LogInformation("Successfully sent message to {email}", context.Message.ManagerEmail);
    }
}