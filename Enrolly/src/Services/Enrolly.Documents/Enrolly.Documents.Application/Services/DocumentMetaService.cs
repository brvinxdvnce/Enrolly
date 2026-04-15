using Enrolly.Documents.Application.Abstractions;
using Enrolly.Documents.Application.DTOs;
using Enrolly.Documents.Application.Mappers;
using Enrolly.Documents.Domain.Entities;
using Enrolly.Documents.Domain.Repositories;

namespace Enrolly.Documents.Application.Services;

public class DocumentMetaService : IDocumentsService
{
    private readonly IDiplomaRepository _diplomaRepository;
    private readonly EducationDocumentMapper _educationDocumentMapper;
    
    public DocumentMetaService(EducationDocumentMapper educationDocumentMapper, IDiplomaRepository diplomaRepository)
    {
        _educationDocumentMapper = educationDocumentMapper;
        _diplomaRepository = diplomaRepository;
    }

    public async Task CreateDocumentMeta(Guid userId, DiplomaMetaDto dto)
    {
        var model = _educationDocumentMapper.FromDto(dto);
        await _diplomaRepository.CreateAsync(model);
    }

    public async Task<DiplomaMetaDto?> GetDocumentMeta(Guid id)
    {
        var doc = await _diplomaRepository.GetByIdAsync(id);
        return _educationDocumentMapper.ToDto(doc);
    }

    public async Task UpdateDocumentMeta(DiplomaMetaDto dto)
    {
        var model = _educationDocumentMapper.FromDto(dto);
        await _diplomaRepository.UpdateAsync(model);
    }

    public async Task DeleteDocumentMeta(Guid id)
    {
        await _diplomaRepository.DeleteAsync(id);
    }
}