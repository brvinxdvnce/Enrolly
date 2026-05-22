using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using Serilog.Templates;
using Serilog.Templates.Themes;

namespace Enrolly.Shared.Logging.Logging;

public static class LoggingExtensions
{
    public static IHostApplicationBuilder AddObservability(
        this IHostApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        
        var serviceName = configuration["Observability:ServiceName"] ?? "Empty Name";
        var serviceVersion =  configuration["Observability:ServiceVersion"] ?? "Empty Version";
        var otlpEndpoint = configuration["Observability:OtlpEndpoint"] ?? "http://localhost:4317";
        var seqUrl = configuration["Observability:SeqUrl"] ?? "http://localhost:5341";

        builder.Services.AddSerilog((services, lc) => lc
            .ReadFrom.Configuration(configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithProperty("ServiceName", serviceName)
            .WriteTo.Console(new ExpressionTemplate(
                "[{@t:HH:mm:ss} {@l:u3}] {@m} {#if IsDefined(@x)}\n{@x}{#end}\n",
                theme: TemplateTheme.Code))
            .WriteTo.Seq(seqUrl));

        return builder;
    }
}