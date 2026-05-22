using System.IdentityModel.Tokens.Jwt;
using Enrolly.Documents.Application.Authorization.Requirements;
using Enrolly.Documents.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Documents.Infrastructure.Authorization.Handlers;

public class OwnerOrManagerAdmissionEditHandler : AuthorizationHandler<OwnerOrManagerRequirement, Guid>
{
    private readonly DocumentsDbContext _dbContext;

    public OwnerOrManagerAdmissionEditHandler(DocumentsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OwnerOrManagerRequirement requirement,
        Guid documentId)
    {
        var actorIdStr = context.User.Claims
            .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

        if (!Guid.TryParse(actorIdStr, out var actorId))
            return;
        
        var hasAccess = await _dbContext.Diplomas
            .AsNoTracking()
            .AnyAsync(d => d.Id == documentId
                      && (d.ApplicantId == actorId 
                      || _dbContext.Applicants.Any(a => a.Id == d.ApplicantId
                        && a.Managers.Any(m => m.Id == actorId))));

        if (!hasAccess) return;
        
        context.Succeed(requirement);
    }
}