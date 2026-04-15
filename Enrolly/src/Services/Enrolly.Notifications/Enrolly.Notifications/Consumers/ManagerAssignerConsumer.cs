using Enrolly.Contracts.Events.Events;
using Enrolly.Notifications.Models.Notifications;
using Enrolly.Notifications.Services.Interfaces;
using MassTransit;

namespace Enrolly.Notifications.Consumers;

public class ManagerAssignerConsumer : IConsumer<ManagerAssignedToApplicationEvent>
{
    private readonly IMailService _mailService;

    public ManagerAssignerConsumer(IMailService mailService)
    {
        _mailService = mailService;
    }

    public async Task Consume(ConsumeContext<ManagerAssignedToApplicationEvent> context)
    {
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
    }
}