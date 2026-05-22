using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events.Events.Dictionary.EducationLevelEvents;

public record EducationLevelUpdatedEvent(
    int Id,
    string Name) : IEvent;