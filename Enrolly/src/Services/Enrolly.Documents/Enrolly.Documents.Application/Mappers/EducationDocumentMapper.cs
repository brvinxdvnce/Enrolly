using Enrolly.Documents.Application.DTOs;
using Enrolly.Documents.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Enrolly.Documents.Application.Mappers;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class EducationDocumentMapper
{
    public partial EducationDocument FromDto(DiplomaMetaDto dto);
    public partial DiplomaMetaDto ToDto(EducationDocument dto);
    
    public partial IEnumerable<EducationDocument> FromDtos(IEnumerable<DiplomaMetaDto> dto);
    public partial IEnumerable<DiplomaMetaDto> ToDtos(IEnumerable<EducationDocument> dto);
}