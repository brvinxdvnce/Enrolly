using DictionaryWorker.DTOs;

namespace Enrolly.EduDictionary.Domain.Repositories;

public interface IDocumentTypeRepository
{
    Task<List<DocumentTypeDto>> GetDocumentTypes();
    Task<DocumentTypeDto> GetDocumentTypeById(Guid id);
}