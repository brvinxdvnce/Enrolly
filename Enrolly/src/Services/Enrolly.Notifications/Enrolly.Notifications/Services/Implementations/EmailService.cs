using Enrolly.Notifications.Configurations;
using Enrolly.Notifications.Models.Notifications;
using Enrolly.Notifications.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Enrolly.Notifications.Services.Implementations;

public class EmailService : IMailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly SmtpSettings _smtpSettings;
    
    public EmailService(IOptions<SmtpSettings> smtpSettings, ILogger<EmailService> logger)
    {
        _logger = logger;
        _smtpSettings = smtpSettings.Value;
    }
    
    public async Task SendAsync(IEmailNotification notify)
    {
        try
        {
            var message = new MimeMessage();
            
            message.Subject = notify.Subject;
            message.Date = DateTimeOffset.Now;
            
            message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.Email));
            message.To.Add(MailboxAddress.Parse(notify.To));
            
            var builder = new BodyBuilder { TextBody = notify.Body };
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
            
            _logger.LogInformation($"Sent email to {notify.To}");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while sending email: {message}, full error: {full}", ex.Message, ex);
        }
    }
}