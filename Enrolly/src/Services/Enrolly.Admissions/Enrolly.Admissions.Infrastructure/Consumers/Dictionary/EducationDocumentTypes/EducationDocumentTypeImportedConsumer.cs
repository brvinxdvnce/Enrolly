using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events.Events.Dictionary.DocumentTypeEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Admissions.Infrastructure.Consumers.Dictionary.EducationDocumentTypes;

public class EducationDocumentTypeImportedConsumer : IConsumer<DocumentTypeImportedEvent>
{
    private readonly ILogger<EducationDocumentTypeImportedConsumer> _logger;
    private readonly IEducationDocumentTypeRepository _repository;

    public EducationDocumentTypeImportedConsumer(ILogger<EducationDocumentTypeImportedConsumer> logger, IEducationDocumentTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<DocumentTypeImportedEvent> context)
    {
        var doc = new EducationDocumentType()
        {
            Id = context.Message.Id,
            CreateTime = context.Message.CreatedAt,
            EducationLevelId = context.Message.EducationLevelId,
            Name = context.Message.Name,
            NextEducationLevelIds = context.Message.NextEducationLevelIds
        };

        var result = await _repository.Add(doc);
    }
}