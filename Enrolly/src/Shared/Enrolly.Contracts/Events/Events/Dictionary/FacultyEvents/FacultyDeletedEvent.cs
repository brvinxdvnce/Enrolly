using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events.Events.Dictionary.FacultyEvents;

public record FacultyDeletedEvent(Guid Id) : IEvent;