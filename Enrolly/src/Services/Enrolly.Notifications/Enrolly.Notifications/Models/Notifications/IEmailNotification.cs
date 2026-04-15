namespace Enrolly.Notifications.Models.Notifications;

public interface IEmailNotification
{
    /// <summary> email address </summary>
    string To { get; }
    /// <summary> email title </summary>
    string Subject { get; }
    /// <summary> body of email message </summary>
    string Body { get; }
}