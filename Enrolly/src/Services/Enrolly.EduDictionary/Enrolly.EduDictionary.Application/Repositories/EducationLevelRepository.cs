using DictionaryWorker.DTOs;
using Enrolly.EduDictionary.Application.Database;
using Enrolly.EduDictionary.Application.Mappings;
using Enrolly.EduDictionary.Domain.Enums;
using Enrolly.EduDictionary.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.EduDictionary.Application.Repositories;

public class EducationLevelRepository : IEducationLevelRepository
{
    private readonly DictionaryDbContext _dbContext;
    private readonly EducationLevelMapper _mapper;

    public EducationLevelRepository(EducationLevelMapper mapper, DictionaryDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<List<EducationLevelDto>> GetEducationLevels()
    {
        return _mapper.ToDtos(
            await _dbContext.EducationLevels
                .Where(x => x.RelevanceStatus == RelevanceStatus.Active)
                .AsNoTracking()
                .ToListAsync())
            .ToList();
    }

    public async Task<EducationLevelDto> GetEducationLevelById(int id)
    {
        return _mapper.ToDto(
            await _dbContext.EducationLevels
                .Where(x => x.RelevanceStatus == RelevanceStatus.Active)
                .AsNoTracking()
                .FirstOrDefaultAsync(el => 
                    el.Id == id));
    }
}