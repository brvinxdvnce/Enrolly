using System.Net.Http.Headers;
using System.Text;
using DictionaryWorker;
using Enrolly.Contracts.Events.Abstractions;
using Enrolly.EduDictionary.Application.Database;
using Enrolly.EduDictionary.Application.DependencyInjection;
using Enrolly.EduDictionary.Application.Repositories;
using Enrolly.EduDictionary.Domain.Repositories;
using Enrolly.Shared.Logging;
using Enrolly.Shared.Logging.Logging;
using Enrolly.Shared.Logging.Utils.Configurations;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.AddObservability();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<RabbitConfiguration>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddScoped<DomainEventInterceptor>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddHostedService<DictionaryUpdateWorker>();

builder.Services.AddHttpClient("1c-mockup.kreosoft.client", client =>
{
    client.BaseAddress = new Uri("https://1c-mockup.kreosoft.space");
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
        "Basic",
        Convert.ToBase64String(
            Encoding.ASCII.GetBytes(
                builder.Configuration.GetConnectionString("KreosoftConnection") 
                ?? string.Empty)));
});

builder.Services.AddDbContext<DictionaryDbContext>((sp, options) => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"))
        .AddInterceptors(sp.GetRequiredService<DomainEventInterceptor>()));

builder.Services.AddRepositories();
builder.Services.AddMappers();
builder.Services.AddServices();
builder.Services.AddMessaging();

//builder.Services.AddSerilogLogging(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
