using System.Security.Authentication;
using Enrolly.Accounts.Application.Services.Interfaces;
using Enrolly.Accounts.Domain.Entities;
using Enrolly.Accounts.Presentation.DTOs;
using Enrolly.Shared.Logging;
using Enrolly.Shared.Logging.Utils.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Enrolly.Accounts.Application.Services.Implementations;

public class CredentialsService : ICredentialsService
{
    private readonly ILogger<CredentialsService> _logger;
    private readonly UserManager<User> _userManager;

    public CredentialsService(UserManager<User> userManager, ILogger<CredentialsService> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }

    public async Task ResetPassword(ChangePasswordRequestDto request)
    {
        if (request.OldPassword == request.NewPassword)
            throw new InvalidCredentialException();
            
        var user = await _userManager.FindByEmailAsync(request.Email) 
                   ?? throw new NotFoundException();

        if (!await _userManager.CheckPasswordAsync(user, request.OldPassword))
            throw new InvalidCredentialException();
        
        var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
        if (!result.Succeeded)
        {
            _logger.LogError("Failed to reset password. Email: {email}, Errors: {errors}",
                user.Email, result.Errors.Select(e => e.Description));

            throw new InvalidCredentialException();
        }
    }

    public async Task ResetEmail(ChangeEmailRequestDto request)
    {
        if (request.OldEmail == request.NewEmail)
            throw new InvalidCredentialException();
        
        var user = await _userManager.FindByEmailAsync(request.OldEmail)
                   ?? throw new NotFoundException("");

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            throw new InvalidCredentialException();
        
        var token = await _userManager.GenerateChangeEmailTokenAsync(user, request.NewEmail);
        var result = await _userManager.ChangeEmailAsync(user, request.NewEmail, token);
        if (!result.Succeeded)
        {
            _logger.LogError("Failed to reset email. Email: {email}, Errors: {errors}",
                user.Email, result.Errors.Select(e => e.Description));
            
            throw new InvalidCredentialException();
        }
    }
}