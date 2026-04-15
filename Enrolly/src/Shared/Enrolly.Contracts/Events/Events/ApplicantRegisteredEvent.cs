using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events;

public record ApplicantRegisteredEvent(
    Guid UserId,
    string FullName,
    string Email
    ) : IEvent;