namespace Enrolly.Notifications.Configurations;

public class SmtpSettings
{
    public string? Host { get; set; }
    public int Port { get; set; }
    public string? SenderName { get; set; }
    public string? Email { get; set; }
    public string? Title { get; set; }
    public string? Password { get; set; }
}