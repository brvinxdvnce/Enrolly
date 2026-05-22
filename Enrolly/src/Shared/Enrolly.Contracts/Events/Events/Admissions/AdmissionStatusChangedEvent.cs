using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events;

public record AdmissionStatusChangedEvent(
    Guid AdmissionId,
    string ApplicantEmail,
    string ApplicantName,
    string NewStatus
    ) : IEvent;