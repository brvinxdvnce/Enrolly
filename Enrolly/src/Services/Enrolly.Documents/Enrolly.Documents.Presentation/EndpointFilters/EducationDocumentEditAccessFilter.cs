using Enrolly.Documents.Application.Authorization.Requirements;
using MassTransit.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Enrolly.Documents.Presentation.EndpointFilters;

public class EducationDocumentEditAccessFilter : IAsyncActionFilter
{
    private readonly IAuthorizationService _authorizationService;

    public EducationDocumentEditAccessFilter(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context, 
        ActionExecutionDelegate next)
    {
        if ((!context.HttpContext.Request.RouteValues.TryGetValue("documentId", out var documentIdObj)
             || !Guid.TryParse(documentIdObj.ToString(), out var documentId))
             || (!context.HttpContext.Request.RouteValues.TryGetValue("applicantId", out var applicantIdObj)
                || !Guid.TryParse(applicantIdObj.ToString(), out var applicantId)))
        {
            context.Result = new ForbidResult();
            return;
        }

        var authResult = await _authorizationService.AuthorizeAsync(
            context.HttpContext.User,
            documentId,
            new OwnerOrManagerRequirement());

        if (!authResult.Succeeded)
        {
            context.Result = new ForbidResult();
            return;
        }
        
        await next();
    }
}