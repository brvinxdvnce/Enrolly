using DictionaryWorker.DTOs;
using Enrolly.EduDictionary.Application.Database;
using Enrolly.EduDictionary.Application.Mappings;
using Enrolly.EduDictionary.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.EduDictionary.Application.Repositories;

public class ProgramRepository : IProgramRepository
{
    private readonly DictionaryDbContext _dbContext;
    private readonly ProgramMapper _mapper;

    public ProgramRepository(DictionaryDbContext dbContext, ProgramMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<ProgramDto>> GetPrograms()
    {
        return _mapper.ToDtos(
            await _dbContext.Programs
                .AsNoTracking()
                .ToListAsync())
            .ToList();
    }

    public async Task<ProgramDto> GetProgramById(Guid id)
    {
        return _mapper.ToDto(
                await _dbContext.Programs
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => 
                        p.Id == id));
    }
}