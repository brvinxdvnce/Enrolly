using Enrolly.Admissions.Infrastructure.Database;
using Enrolly.Auth.Authentication.Politics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Admissions.Presentation.EndpointFilters;

public class AdmissionEditOwnerOrManagerAccessFilter : IEndpointFilter
{
    private readonly IAuthorizationService _authorizationService;
    private readonly AdmissionsDbContext _dbContext;
    
    public AdmissionEditOwnerOrManagerAccessFilter(IAuthorizationService authorizationService, AdmissionsDbContext dbContext)
    {
        _authorizationService = authorizationService;
        _dbContext = dbContext;
    }

    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context, 
        EndpointFilterDelegate next)
    {
        if (!context.HttpContext.Request.RouteValues
                .TryGetValue("admissionId", out var admissionIdObj)
            || admissionIdObj is null
            || !Guid.TryParse(admissionIdObj.ToString(), out var admissionId))
            return Results.BadRequest();
        
        var exists = await _dbContext.Admissions.AnyAsync(a => a.Id == admissionId);
        if (!exists)
            return Results.NotFound($"Admission with id {admissionId} does not exist");
        
        var auth = await _authorizationService
            .AuthorizeAsync(
                context.HttpContext.User,
                admissionId,
                AuthPolicies.CaseFileEdit);
        
        if (!auth.Succeeded)
            return Results.Forbid();
        
        return await next(context); 
    }
}