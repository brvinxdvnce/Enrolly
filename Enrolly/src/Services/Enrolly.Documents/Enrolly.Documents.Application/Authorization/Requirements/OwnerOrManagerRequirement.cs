using Microsoft.AspNetCore.Authorization;

namespace Enrolly.Documents.Application.Authorization.Requirements;

public record OwnerOrManagerRequirement() : IAuthorizationRequirement;