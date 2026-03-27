namespace Enrolly.Notifications.Models.Notifications;

public class AdmissionStatusChangedNotification(string email, string userName) : IEmailNotification
{
    public string To => email;
    public string Subject => "";
    public string Body => "";
}
