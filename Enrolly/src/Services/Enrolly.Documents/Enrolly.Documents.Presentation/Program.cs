using Enrolly.Documents.Application.DependencyInjection;
using Enrolly.Documents.Infrastructure.Configurations;
using Enrolly.Documents.Infrastructure.Database;
using Enrolly.Documents.Infrastructure.DependencyInjection;
using Enrolly.Shared.Logging.Utils.Configurations;
using Enrolly.Shared.Logging.Utils.Middleware;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.Configure<MinIOSettings>(builder.Configuration.GetSection("MinIOConnection"));
builder.Services.Configure<RabbitConfiguration>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.RegisterMinIO();
builder.Services.RegisterServices();
builder.Services.AddRepositories();

builder.Services.AddDbContext<DocumentsDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
