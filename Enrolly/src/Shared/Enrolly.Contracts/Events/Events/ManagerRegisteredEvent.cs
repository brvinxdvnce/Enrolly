using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events.Events;

public record ManagerRegisteredEvent(
    Guid ManagerId,
    string ManagerEmail,
    string ManagerName) : IEvent;