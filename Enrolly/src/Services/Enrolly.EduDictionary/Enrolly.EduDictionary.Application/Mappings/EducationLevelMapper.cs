using DictionaryWorker.DTOs;
using Enrolly.EduDictionary.Domain.Entities;
using Enrolly.EduDictoinary.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Enrolly.EduDictionary.Application.Mappings;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class EducationLevelMapper
{
    public partial EducationLevelDto ToDto(EducationLevel? model);
    public partial IEnumerable<EducationLevelDto> ToDtos(IEnumerable<EducationLevel>? models);
    
    public partial EducationLevel FromDto(EducationLevelDto? dto);
    public partial IEnumerable<EducationLevel> FromDtos(IEnumerable<EducationLevelDto> dtos);
}