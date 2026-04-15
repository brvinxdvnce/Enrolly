using Enrolly.Accounts.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Enrolly.Accounts.Application.Services.Implementations;

public class EmailSender : IEmailSender<User>
{
    public Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
    {
        throw new NotImplementedException();
    }

    public Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
    {
        throw new NotImplementedException();
    }

    public Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
    {
        throw new NotImplementedException();
    }

    public Task SendRoleChangedNotifyAsync()
    {
        throw new NotImplementedException();
    }
}