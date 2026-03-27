using Enrolly.Notifications;
using Enrolly.Notifications.Configurations;
using Enrolly.Notifications.Services.Implementations;
using Enrolly.Notifications.Services.Interfaces;
using Enrolly.Notifications.Workers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();

builder.Services.AddTransient<IMailService, EmailService>();

builder.Services.Configure<SmtpSettings>
    (builder.Configuration.GetSection("SmtpSettings"));

var host = builder.Build();

host.Run();
