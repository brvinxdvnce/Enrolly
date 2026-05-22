using Enrolly.Documents.Infrastructure.Consumers;
using Enrolly.Documents.Infrastructure.Consumers.DocumentType;
using Enrolly.Documents.Infrastructure.Consumers.Users;
using Enrolly.Documents.Infrastructure.Database;
using Enrolly.Shared.Logging.Utils.Configurations;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Enrolly.Documents.Infrastructure.DependencyInjection;

public static class AddMessagingExtension
{
    public static IServiceCollection AddMessaging(this IServiceCollection services)
    {   
        services.AddMassTransit(c =>
        {
            c.AddConsumer<DocumentTypeDeletedConsumer>();
            c.AddConsumer<DocumentTypeImportedConsumer>();
            c.AddConsumer<DocumentTypeUpdatedConsumer>();
            
            c.AddConsumer<ApplicantRegisteredConsumer>();
            c.AddConsumer<ApplicantDeletedConsumer>();

            c.AddConsumer<ManagerRegisteredConsumer>();
            c.AddConsumer<ManagerDeletedConsumer>();
            c.AddConsumer<ManagerGradeChangedConsumer>();
            
            c.AddEntityFrameworkOutbox<DocumentsDbContext>(cfg => 
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