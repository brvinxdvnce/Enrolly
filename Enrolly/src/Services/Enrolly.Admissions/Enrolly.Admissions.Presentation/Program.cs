using Enrolly.Admissions.Application.Authorization.Requirements;
using Enrolly.Admissions.Application.DependencyInjection;
using Enrolly.Admissions.Application.Settings;
using Enrolly.Admissions.Infrastructure.Database;
using Enrolly.Admissions.Infrastructure.DependencyInjection;
using Enrolly.Admissions.Presentation.DependencyInjection;
using Enrolly.Admissions.Presentation.Extensions;
using Enrolly.Auth.Authentication;
using Enrolly.Auth.Authentication.Extensions;
using Enrolly.Contracts.Events.Abstractions;
using Enrolly.Shared.Logging;
using Enrolly.Shared.Logging.Logging;
using Enrolly.Shared.Logging.Utils.Configurations;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.AddObservability();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<AdmissionSettings>(builder.Configuration.GetSection("AdmissionSettings"));
builder.Services.Configure<RabbitConfiguration>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddJwtAuthentication();
builder.Services.AddAuthPolitics();
builder.Services.AddAuthHanders();

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddRepositories();

builder.Services.AddMessaging();

builder.Services.AddScoped<DomainEventInterceptor>();
builder.Services.AddDbContext<AdmissionsDbContext>((sp, options) => 
    options.UseNpgsql(connectionString)
        .AddInterceptors(sp.GetRequiredService<DomainEventInterceptor>()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.AddEndpoints();

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
