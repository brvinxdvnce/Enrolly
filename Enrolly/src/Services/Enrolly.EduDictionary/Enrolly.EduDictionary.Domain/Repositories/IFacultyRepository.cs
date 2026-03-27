using DictionaryWorker.DTOs;

namespace Enrolly.EduDictionary.Domain.Repositories;

public interface IFacultyRepository
{
    Task<List<FacultyDto>> GetFaculties();
    Task<FacultyDto> GetFacultyById(Guid id);
}