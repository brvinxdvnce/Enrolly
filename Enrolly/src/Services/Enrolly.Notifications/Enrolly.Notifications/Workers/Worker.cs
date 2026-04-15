using Enrolly.Notifications.Models.Notifications;
using Enrolly.Notifications.Services.Interfaces;

namespace Enrolly.Notifications.Workers;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IMailService _mailService;
    
    public Worker(ILogger<Worker> logger, IMailService mailService)
    {
        _logger = logger;
        _mailService = mailService;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _mailService.SendAsync(
            new DefaultNotification(
                "souka.tanatos@gmail.com",
                "Полинка"));
        
        while (!stoppingToken.IsCancellationRequested)
        {
            /*if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Task.Delay(1000, stoppingToken);*/
        }
    }
}


