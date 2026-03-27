using DictionaryWorker.DTOs;
using Enrolly.EduDictoinary.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Enrolly.EduDictionary.Application.Mappings;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class ProgramMapper
{
    public partial ProgramDto ToDto(Program? model);
    public partial IEnumerable<ProgramDto> ToDtos(IEnumerable<Program>? models);
    
    public partial Program FromDto(ProgramDto? dto);
    public partial IEnumerable<Program> FromDtos(IEnumerable<ProgramDto> dtos);
}