using Enrolly.Accounts.Application.Services.Implementations;
using Enrolly.Accounts.Application.Services.Interfaces;
using Enrolly.Accounts.Domain.Entities;
using Enrolly.Accounts.Infrastructure;
using Enrolly.Accounts.Infrastructure.Database;
using Enrolly.Accounts.Infrastructure.DependencyInjection;
using Enrolly.Accounts.Infrastructure.Seeders;
using Enrolly.Accounts.Presentation.Extensions;
using Enrolly.Shared.Logging;
using Enrolly.Shared.Logging.Utils.Configurations;
using Enrolly.Shared.Logging.Utils.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<RabbitConfiguration>(builder.Configuration.GetSection("RabbitMQ"));


builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddMessaging();

builder.Services.AddDbContext<UsersDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddDataProtection();

builder.Services.AddIdentityCore<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;

    options.ClaimsIdentity.RoleClaimType = "roles"; 
}) 
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<UsersDbContext>()
    .AddDefaultTokenProviders();

/*
builder.Services.AddAuthScheme();
*/

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

/*
app.UseAuthentication();
app.UseAuthorization();
*/

app.MapControllers();

//app.MapGroup("api/v1/auth").MapIdentityApi<User>();

app.MapGroup("api/v1/auth").MapCustomIdentityApiUsingJwt<User>();

//await app.Services.SeedRoles();

app.Run();