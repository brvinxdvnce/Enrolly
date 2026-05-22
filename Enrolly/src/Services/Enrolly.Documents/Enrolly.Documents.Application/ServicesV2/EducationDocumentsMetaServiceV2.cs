using CSharpFunctionalExtensions;
using Enrolly.Documents.Application.Abstractions.ServicesV2;
using Enrolly.Documents.Application.DTOs;
using Enrolly.Documents.Application.Mappers;
using Enrolly.Documents.Domain.Repositories;

namespace Enrolly.Documents.Application.ServicesV2;

public class EducationDocumentsMetaServiceV2 : IEducationDocumentsMetaServiceV2
{
    private readonly IEducationDocumentRepositoryV2 _educationDocumentRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly EducationDocumentMapper _eduDocMapper;
    
    public EducationDocumentsMetaServiceV2(
        IEducationDocumentRepositoryV2 educationDocumentRepository,
        IApplicantRepository applicantRepository,
        EducationDocumentMapper eduDocMapper)
    {
        _educationDocumentRepository = educationDocumentRepository;
        _applicantRepository = applicantRepository;
        _eduDocMapper = eduDocMapper;
    }

    public async Task<Result<Guid>> CreateDocumentMeta(Guid applicantId, EducationDocumentMetaCreateDto dto)
    {
        return await Result.Success()
            .Ensure(async () => await _applicantRepository.IsActive(applicantId))
            .Bind(async () => await _educationDocumentRepository.CreateAsync(_eduDocMapper.FromCreateDto(dto)));
    }

    public async Task<Result<IReadOnlyCollection<EducationDocumentMetaDto>>> GetAllDocumentsByApplicantId(Guid applicantId)
    {
        return await Result.Success()
            .Ensure(async () => await _applicantRepository.IsActive(applicantId))
            .Bind(async () => await _educationDocumentRepository.GetAllByUserIdAsync(applicantId))
            .Map(educationDocuments => 
                (IReadOnlyCollection<EducationDocumentMetaDto>)_eduDocMapper.ToDtos(educationDocuments));
    }

    public async Task<Result<EducationDocumentMetaDto>> GetDocumentMeta(Guid documentId)
    {
        return await _educationDocumentRepository.GetByIdAsync(documentId)
            .Map(document => _eduDocMapper.ToDto(document));
    }

    public async Task<Result> UpdateDocumentMeta(Guid applicantId, EducationDocumentMetaDto dto)
    {
        return await Result.Success()
            .Ensure(async () => await _applicantRepository.IsActive(applicantId))
            .Bind(async () => 
                await _educationDocumentRepository.UpdateAsync(_eduDocMapper.FromDto(dto)));
    }

    public async Task<Result> DeleteDocumentMeta(Guid applicantId, Guid documentId)
    {
        return await Result.Success()
            .Ensure(async () => await _applicantRepository.IsActive(applicantId))
            .Bind(async () => await _educationDocumentRepository.DeleteAsync(documentId));
    }
}
