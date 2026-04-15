using Enrolly.Documents.Application.Abstractions;
using Enrolly.Documents.Infrastructure.Configurations;
using Enrolly.Documents.Infrastructure.FileStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minio;

namespace Enrolly.Documents.Infrastructure.DependencyInjection;

public static class MinIO
{
    public static IServiceCollection RegisterMinIO(
        this IServiceCollection services)
    {
        services.AddSingleton<IMinioClient>(sp =>
        {
            var configuration = sp.GetRequiredService<IOptions<MinIOSettings>>().Value;
            return new MinioClient()
                .WithEndpoint(configuration.Address)
                .WithCredentials(configuration.AccessKey, configuration.SecretKey)
                .WithSSL(configuration.WithSSL)
                .WithRegion(configuration.Region)
                .Build();
        });

        services.AddScoped<IFileStorageService, MinIOStorageService>();
        return services;
    }
}