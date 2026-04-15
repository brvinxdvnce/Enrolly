namespace Enrolly.Notifications.Models.Notifications;

public class SubAccountCreatedNotification : IEmailNotification
{
    private SubAccountCreatedNotification(string email, string subject, string message)
    {
        To = email;
        Subject = subject;
        Body = message;
    }

    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }

    public static SubAccountCreatedNotification ToManager(
        string managerEmail,
        string managerName)
    {
        return new SubAccountCreatedNotification(
            managerEmail,
            "Enrolly - Назначение менеджером.",
            $"{managerName}, вы были назначены менеджером в Enrolly. Трудитесь честно!");
    }
        
    public static SubAccountCreatedNotification ToApplicant(
        string applicantEmail,
        string applicantName)
    {
        return new SubAccountCreatedNotification(
            applicantEmail,
            "Enrolly - создание профиля абитуриента.",
            $"{applicantName}, поздравляем вас с началом вашего поступления!");
    }
}