using Microsoft.AspNetCore.Authorization;

namespace Enrolly.Admissions.Application.Authorization.Requirements;

public record OwnerOrManagerRequirement() : IAuthorizationRequirement;