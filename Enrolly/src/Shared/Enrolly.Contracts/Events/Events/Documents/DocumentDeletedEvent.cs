using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events.Events;

public record DocumentDeletedEvent(Guid ApplicantId, Guid DocumentId) : IEvent;