using Enrolly.Contracts.Events.Events;
using Enrolly.Notifications.Models.Notifications;
using Enrolly.Notifications.Services.Interfaces;
using MassTransit;

namespace Enrolly.Notifications.Consumers;

public class ManagerAssignerConsumer : IConsumer<ManagerAssignedToAdmissionEvent>
{
    private readonly IMailService _mailService;
    private readonly ILogger<ManagerAssignerConsumer> _logger;

    public ManagerAssignerConsumer(IMailService mailService, ILogger<ManagerAssignerConsumer> logger)
    {
        _mailService = mailService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ManagerAssignedToAdmissionEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Admission Id: {ApplicantId}, Manager Id : {ManagerId}, ApplicantId: {ApplicantId}",
            nameof(ManagerAssignedToAdmissionEvent),
            context.Message.AdmissionId,
            context.Message.ManagerId,
            context.Message.ApplicantId);
        
        await _mailService.SendAsync(
            ManagerAssignerToAdmissionsNotification
                .ToManager(
                    context.Message.ManagerEmail,
                    context.Message.ManagerName,
                    context.Message.ApplicantName
                    ));
        
        await _mailService
            .SendAsync(
                ManagerAssignerToAdmissionsNotification
                    .ToApplicant(
                        context.Message.ApplicantEmail,
                        context.Message.ApplicantName,
                        context.Message.ManagerName
                        ));
        
        
        _logger.LogInformation("Successfully sent message to {email}", context.Message.ManagerEmail);
        
        _logger.LogInformation("Successfully sent message to {email}", context.Message.ApplicantEmail);
    }
}