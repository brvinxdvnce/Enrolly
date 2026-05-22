using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events.Events;

public record ApplicantRegisteredEvent(
    Guid ApplicantId,
    string ApplicantName,
    string Email
    ) : IEvent;