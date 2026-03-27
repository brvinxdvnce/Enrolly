using Enrolly.Accounts.Domain.Entities;
using Enrolly.Accounts.Infrastructure;
using Enrolly.Accounts.Infrastructure.Database;
using Enrolly.Accounts.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddDbContext<UsersDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddIdentityApiEndpoints<User>()
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<UsersDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
    options.ClaimsIdentity.RoleClaimType = "roles");

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGroup("api/v1/auth").MapIdentityApi<User>();

//await app.Services.SeedRoles();

app.Run();