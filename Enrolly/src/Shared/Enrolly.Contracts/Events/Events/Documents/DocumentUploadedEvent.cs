using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events.Events.Documents;

public record DocumentUploadedEvent(Guid UserId, Guid DocumentId, Guid DocumentTypeId) : IEvent;