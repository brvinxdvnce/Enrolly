using Enrolly.Admissions.Presentation.EndpointFilters;

namespace Enrolly.Admissions.Presentation.Extensions;

public static class AuthorizationExtensions
{
    public static RouteHandlerBuilder RequireAdmissionEditAccess(this RouteHandlerBuilder builder)
    {
        return builder.AddEndpointFilter<AdmissionEditOwnerOrManagerAccessFilter>();
    }
}