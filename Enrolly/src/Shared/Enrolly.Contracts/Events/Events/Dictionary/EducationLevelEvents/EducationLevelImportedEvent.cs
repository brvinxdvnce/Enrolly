using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events.Events.Dictionary.EducationLevelEvents;

public record EducationLevelImportedEvent(
    int Id,
    string Name) : IEvent;