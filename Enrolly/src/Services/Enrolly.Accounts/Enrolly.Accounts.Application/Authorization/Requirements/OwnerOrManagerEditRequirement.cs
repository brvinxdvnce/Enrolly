using Microsoft.AspNetCore.Authorization;

namespace Enrolly.Accounts.Application.Authorization.Requirements;

public record OwnerOrManagerEditRequirement() : IAuthorizationRequirement;