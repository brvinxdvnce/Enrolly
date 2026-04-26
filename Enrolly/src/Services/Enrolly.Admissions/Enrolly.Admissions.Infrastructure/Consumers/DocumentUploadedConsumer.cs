using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events.Events;
using MassTransit;

namespace Enrolly.Admissions.Infrastructure.Consumers;

public class DocumentUploadedConsumer : IConsumer<DocumentUploadedEvent>
{
    private readonly IDocumentRepository _documentRepository;

    public DocumentUploadedConsumer(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task Consume(ConsumeContext<DocumentUploadedEvent> context)
    {
    }
}