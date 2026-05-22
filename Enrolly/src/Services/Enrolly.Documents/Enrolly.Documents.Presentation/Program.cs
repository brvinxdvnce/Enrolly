using Enrolly.Contracts.Events.Abstractions;
using Enrolly.Documents.Application.DependencyInjection;
using Enrolly.Documents.Infrastructure.Configurations;
using Enrolly.Documents.Infrastructure.Database;
using Enrolly.Documents.Infrastructure.DependencyInjection;
using Enrolly.Documents.Presentation.DependencyInjection;
using Enrolly.Shared.Logging;
using Enrolly.Shared.Logging.Logging;
using Enrolly.Shared.Logging.Utils.Configurations;
using Enrolly.Shared.Logging.Utils.Middleware;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddObservability();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<MinIOSettings>(builder.Configuration.GetSection("MinIOConnection"));
builder.Services.Configure<RabbitConfiguration>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.RegisterMinIO();
builder.Services.RegisterServices();
builder.Services.AddRepositories();

builder.Services.AddMessaging();

builder.Services.AddScoped<DomainEventInterceptor>();
builder.Services.AddDbContext<DocumentsDbContext>((sp, options) => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"))
        .AddInterceptors(sp.GetRequiredService<DomainEventInterceptor>()));

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();
app.AddEndpoints();

app.UseHttpsRedirection();

app.Run();
