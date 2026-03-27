using DictionaryWorker.DTOs;
using Enrolly.EduDictoinary.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Enrolly.EduDictionary.Application.Mappings;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class FacultyMapper
{
    public partial FacultyDto ToDto(Faculty? model);
    public partial IEnumerable<FacultyDto> ToDtos(IEnumerable<Faculty>? models);
    
    public partial Faculty FromDto(FacultyDto? dto);
    public partial IEnumerable<Faculty> FromDtos(IEnumerable<FacultyDto> dtos);
}
