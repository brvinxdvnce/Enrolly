namespace Enrolly.Notifications.Models.Notifications;

public class RegisterNotification(string email, string userName) : IEmailNotification
{
    public string To => email;
    public string Subject => "";
    public string Body => $"{userName} has been registered, out congratulation!";
}