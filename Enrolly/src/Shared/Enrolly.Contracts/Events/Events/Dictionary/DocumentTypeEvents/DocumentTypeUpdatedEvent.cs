using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events.Events.Dictionary.DocumentTypeEvents;

public record DocumentTypeUpdatedEvent(
    Guid Id,
    DateTime CreatedAt,
    string Name,
    int EducationLevelId,
    ICollection<int> NextEducationLevelIds) : IEvent;