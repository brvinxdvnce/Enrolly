namespace Enrolly.Contracts.Events.Events.Other;

public record UserInfoChanged(Guid UserId, string? UserName, string? Email);