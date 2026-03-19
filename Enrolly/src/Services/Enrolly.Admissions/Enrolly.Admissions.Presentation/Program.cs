using Enrolly.Admissions.Application;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DbConnection");

builder.Services.AddDbContext<AdmissionsDbContext>(options => 
    options.UseNpgsql(connectionString)
    );

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
