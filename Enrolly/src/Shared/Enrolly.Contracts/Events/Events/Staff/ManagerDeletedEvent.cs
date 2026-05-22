using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events.Events;

public record ManagerDeletedEvent(Guid ManagerId) : IEvent;