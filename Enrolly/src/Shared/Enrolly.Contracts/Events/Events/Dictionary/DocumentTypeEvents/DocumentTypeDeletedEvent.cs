using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events.Events.Dictionary.DocumentTypeEvents;

public record DocumentTypeDeletedEvent(Guid Id) : IEvent;