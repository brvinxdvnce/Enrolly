using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events.Events.Dictionary.EducationLevelEvents;

public record EducationLevelDeletedEvent(int Id) : IEvent;