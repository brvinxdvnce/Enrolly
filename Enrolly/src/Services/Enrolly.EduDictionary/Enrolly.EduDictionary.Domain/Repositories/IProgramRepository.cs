
using DictionaryWorker.DTOs;
using Enrolly.Shared.Logging;

namespace Enrolly.EduDictionary.Domain.Repositories;

public interface IProgramRepository
{
    Task<PagedResponce<ProgramDto>> GetPrograms(
        Guid? facultyId,
        int? educationLevelId,
        string? educationForm,
        string? language,
        string? programName,
        string? programCode,
        int page = 1,
        int pageSize = 10);
    Task<ProgramDto> GetProgramById(Guid id);
}
