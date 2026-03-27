
using DictionaryWorker.DTOs;

namespace Enrolly.EduDictionary.Domain.Repositories;

public interface IProgramRepository
{
    Task<List<ProgramDto>> GetPrograms();
    Task<ProgramDto> GetProgramById(Guid id);
}
