using Enrolly.EduDictionary.Application.Database;
using Enrolly.Shared.Logging.Utils.Configurations;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Enrolly.EduDictionary.Application.DependencyInjection;

public static class MessagingRegisterExtension
{
    public static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddMassTransit(conf =>
        {
            conf.AddEntityFrameworkOutbox<DictionaryDbContext>(cfg =>
            {
                cfg.UsePostgres();
                cfg.UseBusOutbox();

                cfg.QueryDelay = TimeSpan.FromSeconds(15);
            });
            
            conf.UsingRabbitMq((context, cfg) =>
            {
                var configuration = context.GetRequiredService<IOptions<RabbitConfiguration>>().Value;
                
                cfg.Host(configuration.Host, "/", h =>
                {
                    h.Username(configuration.UserName);
                    h.Password(configuration.Password);
                });
                
                cfg.ConfigureEndpoints(context);
            });
        });
        
        return services;
    }
}