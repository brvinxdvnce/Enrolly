using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Admissions.Infrastructure.Consumers.Documents;

public class DocumentDeletedConsumer : IConsumer<DocumentDeletedEvent>
{
    private readonly ILogger<DocumentDeletedConsumer> _logger;
    private readonly IDocumentRepository _documentRepository;

    public DocumentDeletedConsumer(IDocumentRepository documentRepository, ILogger<DocumentDeletedConsumer> logger)
    {
        _documentRepository = documentRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DocumentDeletedEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {eduLevelId}", 
            nameof(DocumentDeletedEvent),
            context.Message.DocumentId);
        
        var result = await _documentRepository.DeleteAsync(context.Message.DocumentId);
        
        if (result.IsFailure)
            _logger.LogError("Failed to delete Document with Id {DocumentId} of the Applicant with Id {ApplicantId} : {Error}",
                context.Message.DocumentId, 
                context.Message.ApplicantId,
                result.Error);
        else 
            _logger.LogInformation("Successfully deleted Document {DocId} : {AppId}",
                context.Message.DocumentId, 
                context.Message.ApplicantId);
    }
}