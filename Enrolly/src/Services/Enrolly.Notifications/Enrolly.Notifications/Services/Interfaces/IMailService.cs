namespace Enrolly.Notifications.Services.Interfaces;

public interface IMailService
{
    Task SendAsync(string targetEmail, string subject, string body);
}