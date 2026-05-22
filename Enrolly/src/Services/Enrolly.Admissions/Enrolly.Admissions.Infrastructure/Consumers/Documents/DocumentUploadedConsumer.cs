using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events.Events;
using Enrolly.Contracts.Events.Events.Documents;
using MassTransit;

namespace Enrolly.Admissions.Infrastructure.Consumers.Documents;

public class DocumentUploadedConsumer : IConsumer<DocumentUploadedEvent>
{
    private readonly IDocumentRepository _documentRepository;

    public DocumentUploadedConsumer(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task Consume(ConsumeContext<DocumentUploadedEvent> context)
    {
        /*await _documentRepository.AddAsync();*/
    }
}