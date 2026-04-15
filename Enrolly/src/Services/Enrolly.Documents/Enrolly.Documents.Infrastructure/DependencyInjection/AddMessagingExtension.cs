using Enrolly.Documents.Infrastructure.Consumers;
using Enrolly.Shared.Logging.Utils.Configurations;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Enrolly.Documents.Infrastructure.DependencyInjection;

public static class AddMessagingExtension
{
    public static IServiceCollection AddMessaging(IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IOptions<RabbitConfiguration>>().Value;
        
        services.AddMassTransit(x =>
        {
            x.AddConsumer<ApplicantRegisteredConsumer>();
        });
        
        return services;
    }   
}