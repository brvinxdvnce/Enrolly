using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events.Events.Dictionary.DocumentTypeEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Admissions.Infrastructure.Consumers.Dictionary.EducationDocumentTypes;

public class EducationDocumentTypeUpdatedConsumer : IConsumer<DocumentTypeUpdatedEvent>
{
    private readonly ILogger<EducationDocumentTypeUpdatedConsumer> _logger;
    private readonly IEducationDocumentTypeRepository _repository;


    public EducationDocumentTypeUpdatedConsumer(ILogger<EducationDocumentTypeUpdatedConsumer> logger, IEducationDocumentTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<DocumentTypeUpdatedEvent> context)
    {
        
    }
}