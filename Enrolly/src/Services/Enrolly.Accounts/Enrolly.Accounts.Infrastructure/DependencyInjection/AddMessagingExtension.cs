using System.Reflection;
using Enrolly.Accounts.Infrastructure.Database;
using Enrolly.Shared.Logging;
using Enrolly.Shared.Logging.Utils.Configurations;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Enrolly.Accounts.Infrastructure.DependencyInjection;

public static class AddMessagingExtension
{
    public static IServiceCollection AddMessaging(this IServiceCollection services)
    {   
        services.AddMassTransit(c => {
            //c.AddConsumer<Consumer>()
            
            c.AddEntityFrameworkOutbox<UsersDbContext>(cfg => 
            {
                cfg.UsePostgres();
                cfg.UseBusOutbox();
                
                cfg.QueryDelay = TimeSpan.FromSeconds(15);
            });
            
            c.UsingRabbitMq((context, cfg) =>
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
