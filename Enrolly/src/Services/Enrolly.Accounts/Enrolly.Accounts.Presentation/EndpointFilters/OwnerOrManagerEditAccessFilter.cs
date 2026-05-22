using Enrolly.Accounts.Application.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Enrolly.Accounts.Presentation.EndpointFilters;

public class OwnerOrManagerEditAccessFilter : IAsyncActionFilter
{
    private readonly IAuthorizationService _authorizationService;
    private readonly ILogger<OwnerOrManagerEditAccessFilter> _logger;

    public OwnerOrManagerEditAccessFilter(IAuthorizationService authorizationService, ILogger<OwnerOrManagerEditAccessFilter> logger)
    {
        _authorizationService = authorizationService;
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.RouteValues.TryGetValue("applicantId", out var applicantIdObj)
            || !Guid.TryParse(applicantIdObj.ToString(), out var applicantId))
        {
            context.Result = new BadRequestResult();
            return;
        }

        var authResult = await _authorizationService.AuthorizeAsync(
            context.HttpContext.User,
            applicantId,
            new OwnerOrManagerEditRequirement()
            );

        if (!authResult.Succeeded)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
                context.Result = new UnauthorizedResult();
            else context.Result = new ForbidResult();

            return;
        }
        
        await next();
    }
}