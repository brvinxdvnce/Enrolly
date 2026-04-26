using Enrolly.Admissions.Application.DependencyInjection;
using Enrolly.Admissions.Application.Settings;
using Enrolly.Admissions.Infrastructure.Database;
using Enrolly.Admissions.Presentation.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DbConnection");

builder.Services.Configure<AdmissionSettings>(builder.Configuration.GetSection("AdmissionSettings"));

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddServices();

builder.Services.AddDbContext<AdmissionsDbContext>(options => 
    options.UseNpgsql(connectionString)
    );

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
