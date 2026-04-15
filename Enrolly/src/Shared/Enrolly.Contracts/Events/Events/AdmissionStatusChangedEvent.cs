using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events;

public record AdmissionStatusChangedEvent(
    ) : IEvent;