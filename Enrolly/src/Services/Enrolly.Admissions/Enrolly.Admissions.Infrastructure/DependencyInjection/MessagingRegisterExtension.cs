using Enrolly.Admissions.Infrastructure.Consumers.Dictionary.Program;
using Enrolly.Admissions.Infrastructure.Consumers.Users;
using Enrolly.Admissions.Infrastructure.Database;
using Enrolly.Shared.Logging.Utils.Configurations;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Enrolly.Admissions.Infrastructure.DependencyInjection;

public static class MessagingRegisterExtension
{
    public static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddMassTransit(conf =>
        {
            conf.AddConsumers();
            
            conf.AddEntityFrameworkOutbox<AdmissionsDbContext>(cfg =>
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

                cfg.ConfigureAdmissionsEndpoints(context);
                
                cfg.ConfigureEndpoints(context);
            });
        });
        
        return services;
    }
}