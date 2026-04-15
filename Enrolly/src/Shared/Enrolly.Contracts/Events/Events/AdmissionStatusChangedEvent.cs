using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events;

public record AdmissionStatusChangedEvent(
    Guid AdmissionId,
    string applicantEmail,
    string ApplicantName,
    string NewStatus
    ) : IEvent;