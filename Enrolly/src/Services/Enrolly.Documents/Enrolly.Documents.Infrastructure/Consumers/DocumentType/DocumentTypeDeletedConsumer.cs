using Enrolly.Contracts.Events.Events.Dictionary.DocumentTypeEvents;
using Enrolly.Documents.Domain.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Documents.Infrastructure.Consumers.DocumentType;

public class DocumentTypeDeletedConsumer : IConsumer<DocumentTypeDeletedEvent>
{
    private readonly IDocumentTypeRepository _repository;
    private readonly ILogger<DocumentTypeDeletedConsumer> _logger;

    public DocumentTypeDeletedConsumer(ILogger<DocumentTypeDeletedConsumer> logger, IDocumentTypeRepository educationDocumentRepository)
    {
        _logger = logger;
        _repository = educationDocumentRepository;
    }

    public async Task Consume(ConsumeContext<DocumentTypeDeletedEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {eduLevelId}", 
            nameof(DocumentTypeDeletedEvent),
            context.Message.Id);

        var result = await _repository.DeleteById(context.Message.Id);
        
        if (result.IsFailure)
            _logger.LogError("Failed to add Document Type with Id {DocumentTypeId}: {Error}",
                context.Message.Id, result.Error);
        else 
            _logger.LogInformation("Successfully deleted Document Type with Id {EduLevelId}", context.Message.Id);
    }
}