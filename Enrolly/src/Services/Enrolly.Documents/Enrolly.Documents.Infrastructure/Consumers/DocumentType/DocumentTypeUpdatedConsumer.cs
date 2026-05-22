using Enrolly.Contracts.Events.Events.Dictionary.DocumentTypeEvents;
using Enrolly.Documents.Domain.Entities;
using Enrolly.Documents.Domain.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Documents.Infrastructure.Consumers.DocumentType;

public class DocumentTypeUpdatedConsumer : IConsumer<DocumentTypeUpdatedEvent>
{
    private readonly IDocumentTypeRepository _repository;
    private readonly ILogger<DocumentTypeUpdatedConsumer> _logger;

    public DocumentTypeUpdatedConsumer(IDocumentTypeRepository repository, ILogger<DocumentTypeUpdatedConsumer> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DocumentTypeUpdatedEvent> context)
    {
        _logger.LogInformation("Consuming {eventType}, Entity Id: {eduLevelId}", 
            nameof(DocumentTypeUpdatedEvent),
            context.Message.Id);

        var educationDocument = new EducationDocumentType()
        {
            Id = context.Message.Id,
            Name = context.Message.Name,
        };

        var result = await _repository.Update(educationDocument); 
        
        if (result.IsFailure)
            _logger.LogError("Failed to update Document Type with Id {DocumentTypeId}: {Error}",
                context.Message.Id, result.Error);
        else 
            _logger.LogInformation("Successfully updated Document Type with Id {EduLevelId}", context.Message.Id);
    }
}