using Enrolly.Accounts.Domain.Entities;

namespace Enrolly.Accounts.Application.Services.Interfaces;

public interface IJwtProvider
{
    public Task<string> GenerateToken(User user);
}