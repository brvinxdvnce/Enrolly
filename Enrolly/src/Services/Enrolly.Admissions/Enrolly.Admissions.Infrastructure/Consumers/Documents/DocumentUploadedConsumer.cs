using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events.Events;
using Enrolly.Contracts.Events.Events.Documents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Admissions.Infrastructure.Consumers.Documents;

public class DocumentUploadedConsumer : IConsumer<DocumentUploadedEvent>
{
    private readonly ILogger<DocumentUploadedConsumer> _logger;
    private readonly IDocumentRepository _documentRepository;

    public DocumentUploadedConsumer(IDocumentRepository documentRepository, ILogger<DocumentUploadedConsumer> logger)
    {
        _documentRepository = documentRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DocumentUploadedEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}", 
            nameof(DocumentUploadedEvent));
        
        var document = new EducationDocument()
        {
            DocumentId = context.Message.DocumentId,
            UserId = context.Message.UserId,
            DocumentTypeId = context.Message.DocumentTypeId,
        };

        await _documentRepository.AddAsync(document);
    }
}