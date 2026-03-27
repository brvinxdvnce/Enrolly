namespace Enrolly.Notifications.Models.Notifications;

public interface IEmailNotification
{
    string To { get; }
    string Subject { get; }
    string Body { get; }
}