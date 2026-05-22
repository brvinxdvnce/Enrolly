using Enrolly.Documents.Application.DTOs;
using Enrolly.Documents.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Enrolly.Documents.Application.Mappers;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class EducationDocumentMapper
{
    public partial EducationDocument FromCreateDto(EducationDocumentMetaCreateDto dto);
    public partial EducationDocument FromDto(EducationDocumentMetaDto dto);
    public partial EducationDocumentMetaDto ToDto(EducationDocument dto);
    
    public partial IEnumerable<EducationDocument> FromDtos(IEnumerable<EducationDocumentMetaDto> dto);
    public partial IEnumerable<EducationDocumentMetaDto> ToDtos(IEnumerable<EducationDocument> dto);
}