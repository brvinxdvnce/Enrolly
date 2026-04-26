using System.IO.MemoryMappedFiles;
using Enrolly.Admissions.Application.DTOs;
using Enrolly.Admissions.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Enrolly.Admissions.Application.Mappers;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
public partial class AdmissionMapper
{
    public partial Admission FromCreateDto(AdmissionCreateDto dto);
    
    public partial AdmissionViewDto ToViewDto(Admission admission);
}
