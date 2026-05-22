using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events.Events.Dictionary.ProgramEvents;

public record ProgramDeletedEvent(Guid Id) : IEvent;