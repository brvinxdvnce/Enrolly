using DictionaryWorker.DTOs;

namespace Enrolly.EduDictionary.Domain.Repositories;

public interface IEducationLevelRepository
{
    Task<List<EducationLevelDto>> GetEducationLevels();
    Task<EducationLevelDto> GetEducationLevelById(int id);
}