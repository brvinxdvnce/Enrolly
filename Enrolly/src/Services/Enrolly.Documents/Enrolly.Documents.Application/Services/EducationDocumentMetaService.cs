using Enrolly.Contracts.Events.Events;
using Enrolly.Contracts.Events.Events.Documents;
using Enrolly.Documents.Application.Abstractions;
using Enrolly.Documents.Application.Abstractions.Services;
using Enrolly.Documents.Application.DTOs;
using Enrolly.Documents.Application.Mappers;
using Enrolly.Documents.Domain.Entities;
using Enrolly.Documents.Domain.Repositories;

namespace Enrolly.Documents.Application.Services;

public class EducationDocumentMetaService : IEducationDocumentsMetaService
{
    private readonly IEducationDocumentRepository _educationDocumentRepository;
    private readonly EducationDocumentMapper _educationDocumentMapper;
    
    public EducationDocumentMetaService(EducationDocumentMapper educationDocumentMapper, IEducationDocumentRepository educationDocumentRepository)
    {
        _educationDocumentMapper = educationDocumentMapper;
        _educationDocumentRepository = educationDocumentRepository;
    }

    public async Task CreateDocumentMeta(Guid userId, EducationDocumentMetaDto dto)
    {
        var model = _educationDocumentMapper.FromDto(dto);
        model.AddEvent(new DocumentUploadedEvent(userId, model.Id, model.DocumentTypeId));
        await _educationDocumentRepository.CreateAsync(model);
    }

    public async Task<EducationDocumentMetaDto?> GetDocumentMeta(Guid id)
    {
        var doc = await _educationDocumentRepository.GetByIdAsync(id);
        return _educationDocumentMapper.ToDto(doc);
    }

    public async Task UpdateDocumentMeta(EducationDocumentMetaDto dto)
    {
        var model = _educationDocumentMapper.FromDto(dto);
        await _educationDocumentRepository.UpdateAsync(model);
    }

    public async Task DeleteDocumentMeta(Guid id)
    {
        await _educationDocumentRepository.DeleteAsync(id);
    }
}