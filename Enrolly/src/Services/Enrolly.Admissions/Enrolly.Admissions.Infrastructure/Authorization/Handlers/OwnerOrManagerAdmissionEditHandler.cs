using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Enrolly.Admissions.Application.Authorization.Requirements;
using Enrolly.Admissions.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Admissions.Infrastructure.Authorization.Handlers;

public class OwnerOrManagerAdmissionEditHandler : AuthorizationHandler<OwnerOrManagerRequirement, Guid>
{
    private readonly AdmissionsDbContext _dbContext;
    
    public OwnerOrManagerAdmissionEditHandler(AdmissionsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        OwnerOrManagerRequirement requirement,
        Guid admissionId)
    {
        var actorIdClaim = context.User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(actorIdClaim, out var actorId))
            return;
        
        var hasAccess = await _dbContext.Admissions
            .AsNoTracking()
            .AnyAsync(a => a.Id == admissionId &&
                           (a.ApplicantId == actorId ||
                            _dbContext.Applicants
                                .AsNoTracking()
                                .Any(app => 
                                    app.Id == a.ApplicantId &&
                                    app.Managers.Any(manager => manager.Id == actorId))));
        
        if (hasAccess)
            context.Succeed(requirement);
    }
}