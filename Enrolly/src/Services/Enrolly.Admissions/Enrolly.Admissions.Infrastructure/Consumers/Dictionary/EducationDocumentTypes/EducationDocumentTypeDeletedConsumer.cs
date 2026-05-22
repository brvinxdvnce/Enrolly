using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events.Events.Dictionary.DocumentTypeEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Admissions.Infrastructure.Consumers.Dictionary.EducationDocumentTypes;

public class EducationDocumentTypeDeletedConsumer : IConsumer<DocumentTypeDeletedEvent>
{
    private readonly ILogger<EducationDocumentTypeDeletedConsumer> _logger;
    private readonly IEducationDocumentTypeRepository _repository;
    
    public EducationDocumentTypeDeletedConsumer(ILogger<EducationDocumentTypeDeletedConsumer> logger, IEducationDocumentTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<DocumentTypeDeletedEvent> context)
    {
        var result = await _repository.DeleteById(context.Message.Id);
    }
}