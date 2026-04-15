using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events;

public record ApplicantDeletedEvent(Guid Id) : IEvent;