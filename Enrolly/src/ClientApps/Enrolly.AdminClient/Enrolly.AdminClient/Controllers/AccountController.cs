using Enrolly.AdminClient.Models.ViewModels;
using Enrolly.AdminClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.AdminClient.Controllers;

public class AccountController : Controller
{
    private readonly AccountsService _accountsService;

    public AccountController(AccountsService accountsService)
    {
        _accountsService = accountsService;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (Request.Cookies["access-token"] != null)
            return RedirectToAction("Index", "Home");
 
        ViewBag.ReturnUrl = returnUrl;
        return View(new LoginViewModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(model);
 
        var (response, error) = await _accountsService.LoginAsync(model.Email, model.Password);
 
        if (error is not null)
        {
            model.Error = error;
            return View(model);
        }
 
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = Request.IsHttps,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddHours(8),
            Path = "/"
        };
 
        Response.Cookies.Append("access-token", response!.AccessToken, cookieOptions);
 
        Response.Cookies.Append("refresh-token", response.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = Request.IsHttps,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(7),
            Path = "/"
        });
 
        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);
 
        return RedirectToAction("Index", "Home");
    }
 
    [HttpPost]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("access-token");
        Response.Cookies.Delete("refresh-token");
        return RedirectToAction("Login");
    }
}