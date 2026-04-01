using System.Net.Http.Headers;
using System.Text;
using DictionaryWorker;
using Enrolly.EduDictionary.Application.Configuration;
using Enrolly.EduDictionary.Application.Database;
using Enrolly.EduDictionary.Application.Repositories;
using Enrolly.EduDictionary.Domain.Repositories;
using Enrolly.Shared.Logging;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddDbContext<DictionaryDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddRepositories();
builder.Services.AddMappers();
builder.Services.AddServices();

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
