using Enrolly.Notifications.Models.Notifications;

namespace Enrolly.Notifications.Services.Interfaces;

public interface IMailService
{
    Task SendAsync(IEmailNotification botify);
}