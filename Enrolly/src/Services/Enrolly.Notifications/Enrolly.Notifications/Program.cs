using Enrolly.Notifications.Configurations;
using Enrolly.Notifications.Services.Implementations;
using Enrolly.Notifications.Services.Interfaces;
using Enrolly.Notifications.DependencyInjection;
using Enrolly.Notifications.Workers;
using Enrolly.Shared.Logging.Logging;
using Enrolly.Shared.Logging.Utils.Configurations;

var builder = Host.CreateApplicationBuilder(args);

builder.AddObservability();

builder.Services.Configure<RabbitConfiguration>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddMessaging();
//builder.Services.AddHostedService<Worker>();

builder.Services.AddTransient<IMailService, EmailService>();

builder.Services.Configure<SmtpSettings>
    (builder.Configuration.GetSection("SmtpSettings"));

var host = builder.Build();

host.Run();
