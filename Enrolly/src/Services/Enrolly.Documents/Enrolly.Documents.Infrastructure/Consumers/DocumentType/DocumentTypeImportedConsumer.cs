using Enrolly.Contracts.Events.Events.Dictionary.DocumentTypeEvents;
using Enrolly.Documents.Domain.Entities;
using Enrolly.Documents.Domain.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Documents.Infrastructure.Consumers.DocumentType;

public class DocumentTypeImportedConsumer : IConsumer<DocumentTypeImportedEvent>
{
    private readonly IDocumentTypeRepository _repository;
    private readonly ILogger<DocumentTypeImportedConsumer> _logger;

    public DocumentTypeImportedConsumer(IDocumentTypeRepository repository, ILogger<DocumentTypeImportedConsumer> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DocumentTypeImportedEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {eduLevelId}", 
            nameof(DocumentTypeImportedEvent),
            context.Message.Id);

        var educationDocument = new EducationDocumentType()
        {
            Id = context.Message.Id,
            Name = context.Message.Name,
        };

        var result = await _repository.Add(educationDocument); 
        
        if (result.IsFailure)
            _logger.LogError("Failed to add Document Type with Id {DocumentTypeId}: {Error}",
                context.Message.Id, result.Error);
        else 
            _logger.LogInformation("Successfully deleted Document Type with Id {EduLevelId}", context.Message.Id);
    
    }
}