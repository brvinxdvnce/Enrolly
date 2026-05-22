using Enrolly.AdminClient.Services;
using Enrolly.Shared.Logging.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.AddObservability();

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<JwtCookieHandler>();

builder.Services.AddHttpClient<DictionaryService>(client => {
    client.BaseAddress = new Uri(builder.Configuration["Services:Dictionary"] ?? "http://localhost:5075");
}).AddHttpMessageHandler<JwtCookieHandler>();

builder.Services.AddHttpClient<ImportsService>(client => {
    client.BaseAddress = new Uri(builder.Configuration["Services:Dictionary"] ?? "http://localhost:5075");
}).AddHttpMessageHandler<JwtCookieHandler>();

builder.Services.AddHttpClient<AccountsService>(client => {
    client.BaseAddress = new Uri(builder.Configuration["Services:Accounts"] ?? "http://localhost:5204");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
