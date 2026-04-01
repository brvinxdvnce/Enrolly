using DictionaryWorker.DTOs;
using Enrolly.EduDictionary.Application.Database;
using Enrolly.EduDictionary.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Enrolly.EduDictionary.Application.Mappings;
using Enrolly.EduDictionary.Domain.Enums;

namespace Enrolly.EduDictionary.Application.Repositories;

public class FacultyRepository : IFacultyRepository
{
    private readonly FacultyMapper _mapper;
    private readonly DictionaryDbContext _dbContext;

    public FacultyRepository(DictionaryDbContext dbContext, FacultyMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<FacultyDto>> GetFaculties()
    {
        return _mapper.ToDtos(
            await _dbContext.Faculties
                .Where(x => x.RelevanceStatus == RelevanceStatus.Active)
                .AsNoTracking()
                .ToListAsync())
            .ToList();
    }

    public async Task<FacultyDto> GetFacultyById(Guid id)
    {
        return _mapper.ToDto(
            await _dbContext.Faculties
                .Where(x => x.RelevanceStatus == RelevanceStatus.Active)
                .AsNoTracking()
                .FirstOrDefaultAsync(f =>
                    f.Id == id));
    }
}
