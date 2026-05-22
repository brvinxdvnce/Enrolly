using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events.Events;
using MassTransit;

namespace Enrolly.Admissions.Infrastructure.Consumers.Documents;

public class DocumentDeletedConsumer : IConsumer<DocumentDeletedEvent>
{
    private readonly IDocumentRepository _documentRepository;

    public DocumentDeletedConsumer(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task Consume(ConsumeContext<DocumentDeletedEvent> context)
    {
        await _documentRepository.DeleteAsync(context.Message.DocumentId);
    }
}