using Enrolly.Contracts.Events.Events;
using Enrolly.Notifications.Models.Notifications;
using Enrolly.Notifications.Services.Interfaces;
using MassTransit;

namespace Enrolly.Notifications.Consumers;

public class ManagerRegisteredConsumer : IConsumer<ManagerRegisteredEvent>
{
    private readonly IMailService _mailService;

    public ManagerRegisteredConsumer(IMailService mailService)
    {
        _mailService = mailService;
    }

    public async Task Consume(ConsumeContext<ManagerRegisteredEvent> context)
    {
        await _mailService
            .SendAsync(
                SubAccountCreatedNotification
                    .ToManager(
                        context.Message.ManagerEmail,
                        context.Message.ManagerName));
    }
}