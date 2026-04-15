using Enrolly.Accounts.Application.DTOs;
using Enrolly.Accounts.Application.Mappers;
using Enrolly.Accounts.Application.Services.Interfaces;
using Enrolly.Accounts.Domain.Entities;
using Enrolly.Shared.Logging.Utils.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Enrolly.Accounts.Application.Services.Implementations;

public class UserProfileService : IUserProfileService
{
    private readonly UserManager<User> _userManager;
    private readonly UserMapper _mapper;
    
    public UserProfileService(UserManager<User> userManager, UserMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<UserViewDto> GetUserAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString())
            ?? throw new NotFoundException($"User with id {id} not found");
        
        return _mapper.ToView(user);
    }

    public async Task UpdateUserAsync(Guid id, UpdateUserDto updateUser)
    {
        var dbuser = await _userManager.FindByIdAsync(id.ToString())
            ?? throw new NotFoundException($"User with id {id} not found");
        
        dbuser.UserName = updateUser.UserName ?? dbuser.UserName;
        dbuser.PhoneNumber = updateUser.PhoneNumber ?? dbuser.PhoneNumber;
        
        await _userManager.UpdateAsync(dbuser);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString()) 
            ?? throw new NotFoundException($"User with id {id} not found");
        await _userManager.DeleteAsync(user);
    }
}