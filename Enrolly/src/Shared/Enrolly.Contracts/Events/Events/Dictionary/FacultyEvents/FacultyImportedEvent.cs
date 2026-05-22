using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events.Events.Dictionary.FacultyEvents;

public record FacultyImportedEvent(
    Guid Id,
    string Name,
    DateTime CreatedAt
) : IEvent;