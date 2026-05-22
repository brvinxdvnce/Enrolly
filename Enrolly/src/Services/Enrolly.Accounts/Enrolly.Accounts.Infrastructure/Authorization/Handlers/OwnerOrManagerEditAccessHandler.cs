using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Enrolly.Accounts.Application.Authorization.Requirements;
using Enrolly.Accounts.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Enrolly.Accounts.Infrastructure.Authorization.Handlers;

public class OwnerOrManagerEditAccessHandler : AuthorizationHandler<OwnerOrManagerEditRequirement, Guid>
{
    private readonly UsersDbContext _dbContext;
    private readonly ILogger<OwnerOrManagerEditAccessHandler> _logger;

    public OwnerOrManagerEditAccessHandler(UsersDbContext dbContext, ILogger<OwnerOrManagerEditAccessHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OwnerOrManagerEditRequirement editRequirement,
        Guid editableUserId)
    {
        _logger.LogInformation("Processing {handler}", nameof(OwnerOrManagerEditAccessHandler));
        
        //var actorIdClaim = context.User.Claims
        //    .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
        
        var actorIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        _logger.LogInformation("Actor Id from token: {actorIdClaim}", actorIdClaim);
        
        if (!Guid.TryParse(actorIdClaim, out var actorId))
            return;
        
        _logger.LogInformation("Actor: {actorId}, Editable: {edit}", actorId,editableUserId );
        bool isOwner = actorId == editableUserId;
        
        var isManager = await _dbContext.Applicants
            .AsNoTracking()
            .AnyAsync(a => (a.Id == editableUserId)
                && a.Managers.Any(m => m.Id == actorId));


        if (isOwner || isManager)
        {
            context.Succeed(editRequirement);
            return;
        }
        _logger.LogWarning("User with id {actorId} dont have enough permissions to edit user with Id {editableUserId}.",
            actorId,
            editableUserId);
        
    }
}