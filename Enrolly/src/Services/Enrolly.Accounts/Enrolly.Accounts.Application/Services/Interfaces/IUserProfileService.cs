using Enrolly.Accounts.Application.DTOs;

namespace Enrolly.Accounts.Application.Services.Interfaces;

public interface IUserProfileService
{
    public Task<UserViewDto> GetUserAsync(Guid id);
    public Task UpdateUserAsync(Guid id, UpdateUserDto updateUser);
    public Task DeleteUserAsync(Guid id);
}