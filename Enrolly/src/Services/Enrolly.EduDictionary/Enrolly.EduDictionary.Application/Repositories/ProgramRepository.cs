using DictionaryWorker.DTOs;
using Enrolly.EduDictionary.Application.Database;
using Enrolly.EduDictionary.Application.Mappings;
using Enrolly.EduDictionary.Domain.Enums;
using Enrolly.EduDictionary.Domain.Repositories;
using Enrolly.Shared.Logging;
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
    
    public async Task<ProgramDto> GetProgramById(Guid id)
    {
        return _mapper.ToDto(
                await _dbContext.Programs
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => 
                        p.Id == id));
    }

    public async Task<PagedResponce<ProgramDto>> GetPrograms(
        Guid? facultyId,
        int? educationLevelId,
        string? educationForm,
        string? language,
        string? programName,
        string? programCode,
        int page = 1,
        int pageSize = 10
        )
    {
        var programs = _dbContext.Programs
            .Include(p => p.Faculty)
            .Include(p => p.EducationLevel)
            .Where(x => x.RelevanceStatus == RelevanceStatus.Active)
            .AsNoTracking()
            .AsQueryable();
        
        if (facultyId is not null)
            programs = programs.Where(p => p.FacultyId == facultyId);
        
        if (educationLevelId is not null)
            programs = programs.Where(p => p.EducationLevelId == educationLevelId);

        if (educationForm is not null)
            programs = programs.Where(p => p.EducationForm.Contains(educationForm));
        
        if (language is not null)
            programs = programs.Where(p => p.Language.Contains(language));
        
        if (programName is not null)
            programs = programs.Where(p => p.Name.Contains(programName));
        
        if (programCode is not null)
            programs = programs.Where(p => p.Code.Contains(programCode));
        
        var programsCount = await programs.CountAsync();
        
        programs  = programs.Skip((page - 1) * pageSize).Take(pageSize);
        
        return new PagedResponce<ProgramDto>()
        {
            Content = _mapper.ToDtos(await programs.ToListAsync()).ToList(),
            PageNumber = page,
            PageSize = pageSize,
            PagesCount = (int) Math.Ceiling((double) programsCount / pageSize),
            TotalCount = programsCount,
        };
    }
}
