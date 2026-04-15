using Enrolly.Contracts.Events;
using Enrolly.Notifications.Models.Notifications;
using Enrolly.Notifications.Services.Interfaces;
using MassTransit;

namespace Enrolly.Notifications.Consumers;

public class AdmissionStatusChangedConsumer : IConsumer<AdmissionStatusChangedEvent>
{
    private readonly IMailService _mailService;

    public AdmissionStatusChangedConsumer(IMailService mailService)
    {
        _mailService = mailService;
    }

    public async Task Consume(ConsumeContext<AdmissionStatusChangedEvent> context)
    {
        await _mailService
            .SendAsync(
                new AdmissionStatusChangedNotification(
                    context.Message.applicantEmail,
                    context.Message.ApplicantName,
                    context.Message.NewStatus));
    }
}