using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events.Events.Dictionary.FacultyEvents;

public record FacultyUpdatedEvent(
    Guid Id,
    string Name,
    DateTime CreatedAt) : IEvent;