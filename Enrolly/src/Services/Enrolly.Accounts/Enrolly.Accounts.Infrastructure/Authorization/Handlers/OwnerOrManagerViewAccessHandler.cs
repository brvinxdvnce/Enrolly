using System.IdentityModel.Tokens.Jwt;
using Enrolly.Accounts.Application.Authorization.Requirements;
using Enrolly.Accounts.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Accounts.Infrastructure.Authorization.Handlers;

public class OwnerOrManagerViewAccessHandler : AuthorizationHandler<OwnerOrManagerViewRequirement, Guid>
{
    private readonly UsersDbContext _dbContext;

    public OwnerOrManagerViewAccessHandler(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OwnerOrManagerViewRequirement requirement,
        Guid applicantId)
    {
        var actorIdClaim = context.User.Claims
            .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

        if (!Guid.TryParse(actorIdClaim, out var actorId))
            return;

        if (applicantId == actorId)
        {
            context.Succeed(requirement);
            return;
        }
        
        var isManager = await _dbContext.Managers
            .AsNoTracking()
            .AnyAsync(m => m.Id == actorId);

        if (!isManager)
            context.Succeed(requirement);
    }
}