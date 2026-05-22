using Enrolly.Notifications.Consumers;
using Enrolly.Shared.Logging.Utils.Configurations;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Enrolly.Notifications.DependencyInjection;

public static class AddMessagingExtension
{
    public static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IOptions<RabbitConfiguration>>().Value;
        
        services.AddMassTransit(c =>
        {
            c.AddConsumer<ApplicantRegisteredConsumer>();
            c.AddConsumer<ManagerAssignerConsumer>();
            c.AddConsumer<ManagerRegisteredConsumer>();
            c.AddConsumer<AdmissionStatusChangedConsumer>();
            
            c.UsingRabbitMq((context, cfg) => {
                cfg.Host(configuration.Host, "/", host => {
                    host.Username(configuration.UserName);
                    host.Password(configuration.Password);
                });
                
                cfg.ReceiveEndpoint("Enrolly.Notifications.ApplicantRegistered", e => {
                    e.Consumer<ApplicantRegisteredConsumer>(context);
                });
                
                cfg.ReceiveEndpoint("Enrolly.Notifications.ManagerAssigner", e => {
                    e.Consumer<ManagerAssignerConsumer>(context);
                });
                
                cfg.ReceiveEndpoint("Enrolly.Notifications.ManagerRegistered", e => {
                    e.Consumer<ManagerRegisteredConsumer>(context);
                });
                
                cfg.ReceiveEndpoint("Enrolly.Notifications.AdmissionStatusChanged", e => {
                    e.Consumer<AdmissionStatusChangedConsumer>(context);
                });
                
                cfg.ConfigureEndpoints(context);
            });
        });
        
        return services;
    }   
}