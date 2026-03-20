using Enrolly.Notifications.Configurations;
using Enrolly.Notifications.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Enrolly.Notifications.Services.Implementations;

public class EmailService : IMailService
{
    private readonly SmtpSettings _smtpSettings;
    
    public EmailService(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }
    
    public async Task SendAsync(
        string targetEmail,
        string subject,
        string body)
    {
        try
        {
            var message = new MimeMessage();
            
            message.Subject = subject;
            message.Date = DateTimeOffset.Now;
            
            message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.Email));
            message.To.Add(MailboxAddress.Parse(targetEmail));
            
            var builder = new BodyBuilder { TextBody = body };
            message.Body = builder.ToMessageBody();

            using var smtpClient = new SmtpClient();
            
            await smtpClient.ConnectAsync(
                _smtpSettings.Host,
                _smtpSettings.Port,
                SecureSocketOptions.SslOnConnect);

            await smtpClient.AuthenticateAsync(
                _smtpSettings.Email,
                _smtpSettings.Password);
                
            await smtpClient.SendAsync(message);
                
            await smtpClient.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}