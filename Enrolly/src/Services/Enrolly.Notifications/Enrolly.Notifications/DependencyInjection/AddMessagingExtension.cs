using Enrolly.Notifications.Consumers;
using Enrolly.Shared.Logging.Utils.Configurations;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Enrolly.Notifications.DependencyInjection;

public static class AddMessagingExtension
{
    public static IServiceCollection AddMessaging(IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IOptions<RabbitConfiguration>>().Value;
        
        services.AddMassTransit(c =>
        {
            c.AddConsumer<ApplicantRegisteredConsumer>();
            c.AddConsumer<ManagerAssignerConsumer>();
            c.AddConsumer<ManagerRegisteredConsumer>();
            c.AddConsumer<AdmissionStatusChangedConsumer>();
            
            
            c.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration.Host, "/", host =>
                {
                    host.Username(configuration.UserName);
                    host.Password(configuration.Password);
                });
                
                cfg.ConfigureEndpoints(context);
            });
        });
        
        return services;
    }   
}