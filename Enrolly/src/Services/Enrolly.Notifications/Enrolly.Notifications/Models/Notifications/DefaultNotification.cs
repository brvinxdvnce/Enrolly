namespace Enrolly.Notifications.Models.Notifications;

public class DefaultNotification(string email, string userName) : IEmailNotification
{
    public string To => email;
    public string Subject => "Уведомление от Enrolly";
    public string Body => $"{userName}, Привет! Сегодня ничего не произошло";
}