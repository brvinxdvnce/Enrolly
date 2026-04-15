namespace Enrolly.Notifications.Models.Notifications;

public class ManagerAssignerToAdmissionsNotification : IEmailNotification
{
    private ManagerAssignerToAdmissionsNotification(string email, string subject, string message)
    {
        To = email;
        Subject = subject;
        Body = message;
    }

    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }

    public static ManagerAssignerToAdmissionsNotification ToManager(
        string managerEmail,
        string managerName,
        string applicantName)
    {
        return new ManagerAssignerToAdmissionsNotification(
            managerEmail,
            "Enrolly - Назначение ответственным за заявку абитуриента.",
            $"{managerName}, вы были назначены ответственным за заявку " +
            $"на поступление пользователя {applicantName}");
    }
    
    public static ManagerAssignerToAdmissionsNotification ToApplicant(
        string applicantEmail,
        string applicantName,
        string managerName)
    {
        return new ManagerAssignerToAdmissionsNotification(
            applicantEmail,
            "Enrolly - Назначение менеджера на вашу заявку.",
            $"{applicantName}, на одну из ваших заявок был назначен" +
            $" менеджер {managerName}. Посмотрите подробности в личном кабинете.");
    }
}