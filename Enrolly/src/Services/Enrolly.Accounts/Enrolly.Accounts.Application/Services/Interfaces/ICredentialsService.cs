using Enrolly.Accounts.Presentation.DTOs;

namespace Enrolly.Accounts.Application.Services.Interfaces;

public interface ICredentialsService
{
    public Task ResetPassword(ChangePasswordRequestDto request);
    public Task ResetEmail(ChangeEmailRequestDto request);
}