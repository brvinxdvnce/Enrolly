using DictionaryWorker.DTOs;
using Enrolly.EduDictionary.Application.Database;
using Enrolly.EduDictionary.Application.Mappings;
using Enrolly.EduDictionary.Domain.Enums;
using Enrolly.EduDictionary.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.EduDictionary.Application.Repositories;

public class DocumentTypeRepository : IDocumentTypeRepository
{
    private readonly DictionaryDbContext _dbContext;
    private readonly DocumentTypeMapper _mapper;

    public DocumentTypeRepository(DocumentTypeMapper mapper, DictionaryDbContext context)
    {
        _mapper = mapper;
        _dbContext = context;
    }

    public async Task<List<DocumentTypeDto>> GetDocumentTypes()
    {
        return _mapper.ToDtos(
                await _dbContext
                    .DocumentTypes
                    .Where(x => x.RelevanceStatus == RelevanceStatus.Active)
                    .AsNoTracking()
                    .ToListAsync())
            .ToList();
    }

    public async Task<DocumentTypeDto> GetDocumentTypeById(Guid id)
    {
        return _mapper.ToDto(
            await _dbContext
                .DocumentTypes
                .Where(x => x.RelevanceStatus == RelevanceStatus.Active)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id));
    }
}