using Microsoft.AspNetCore.Mvc.Filters;

namespace Enrolly.Documents.Presentation.EndpointFilters;

public class PassportEditAccessFilter : IAsyncActionFilter
{
    public Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        throw new NotImplementedException();
    }
}