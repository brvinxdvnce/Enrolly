using System.Net.Http.Json;
using DictionaryWorker.DTOs;
using Enrolly.EduDictionary.Application.Database;
using Enrolly.EduDictionary.Application.Services.Interfaces;
using Enrolly.EduDictionary.Domain.DTOs;
using Enrolly.EduDictionary.Domain.Entities;
using Enrolly.EduDictionary.Domain.Enums;
using Enrolly.EduDictoinary.Domain.Entities;
using Enrolly.Shared.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Enrolly.EduDictionary.Application.Services.Implementations;

// как работает каждый из методов:
// загружается информация с удаленного сервера, загружается актуальная локальная БД, составляется словарь
// из текущей БД (Id: Object) и циклом по полученным с сервера объектам каждый сравнивается
// с имеющимся в локальной БД (По Id) по следующим правилам:
// объект не найден в локальной бд -> он новый для нас,
// объект найден -> если изменен, то обновляется локальная запись в БД,
// объект из локальной БД не был найден в данных из запроса -> он был удален
// (в текущей системе подразумевается мягкое удаление)
public class ExternalDataCollector : IExternalDataCollector
{
    private readonly ILogger<ExternalDataCollector> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHttpClientFactory _httpClientFactory;
    
    public ExternalDataCollector(
        IServiceScopeFactory scopeFactory,
        ILogger<ExternalDataCollector> logger,
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _scopeFactory = scopeFactory;
    }
    
    public async Task ImportAll(CancellationToken cancellationToken = default)
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DictionaryDbContext>();
            var httpClient = _httpClientFactory.CreateClient("1c-mockup.kreosoft.client");

            var eduLevelSummary = ImportSummary.StartImport<EducationLevel>();
            var res1 = await UpdateEducationLevels(dbContext, httpClient, cancellationToken);
            eduLevelSummary.AddStats(res1);
            eduLevelSummary.StopImport();
                
            var docTypeSummary = ImportSummary.StartImport<DocumentType>();
            var res2 = await UpdateDocumentTypes(dbContext, httpClient, cancellationToken);
            docTypeSummary.AddStats(res2);
            docTypeSummary.StopImport();
                
            var facultySummary = ImportSummary.StartImport<Faculty>();
            var res3 = await UpdateFaculties(dbContext, httpClient, cancellationToken);
            facultySummary.AddStats(res3);
            facultySummary.StopImport();
                
            var programSummary = ImportSummary.StartImport<Program>();
            var res4 = await UpdatePrograms(dbContext, httpClient, cancellationToken);
            programSummary.AddStats(res4);
            programSummary.StopImport();
            
            await dbContext.Imports.AddAsync(eduLevelSummary, cancellationToken);
            await dbContext.Imports.AddAsync(docTypeSummary, cancellationToken);
            await dbContext.Imports.AddAsync(facultySummary, cancellationToken);
            await dbContext.Imports.AddAsync(programSummary, cancellationToken);
            
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured during importing");
        }
    }

    public async Task ImportEducationLevels(CancellationToken cancellationToken = default)
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DictionaryDbContext>();
            var httpClient = _httpClientFactory.CreateClient("1c-mockup.kreosoft.client");

            var eduLevelSummary = ImportSummary.StartImport<EducationLevel>();
            var res1 = await UpdateEducationLevels(dbContext, httpClient, cancellationToken);
            eduLevelSummary.AddStats(res1);
            eduLevelSummary.StopImport();
            
            await dbContext.Imports.AddAsync(eduLevelSummary, cancellationToken);
            
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured during importing");
        }
    }
    
    public async Task ImportDocumentTypes(CancellationToken cancellationToken = default)
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DictionaryDbContext>();
            var httpClient = _httpClientFactory.CreateClient("1c-mockup.kreosoft.client");

            var docTypeSummary = ImportSummary.StartImport<DocumentType>();
            var res2 = await UpdateDocumentTypes(dbContext, httpClient, cancellationToken);
            docTypeSummary.AddStats(res2);
            docTypeSummary.StopImport();
            
            await dbContext.Imports.AddAsync(docTypeSummary, cancellationToken);
            
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured during importing");
        }
    }
    
    public async Task ImportFaculties(CancellationToken cancellationToken = default)
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DictionaryDbContext>();
            var httpClient = _httpClientFactory.CreateClient("1c-mockup.kreosoft.client");

            var facultySummary = ImportSummary.StartImport<Faculty>();
            var res3 = await UpdateFaculties(dbContext, httpClient, cancellationToken);
            facultySummary.AddStats(res3);
            facultySummary.StopImport();

            await dbContext.Imports.AddAsync(facultySummary, cancellationToken);
            
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured during importing");
        }
    }
    
    public async Task ImportPrograms(CancellationToken cancellationToken = default)
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DictionaryDbContext>();
            var httpClient = _httpClientFactory.CreateClient("1c-mockup.kreosoft.client");

            var programSummary = ImportSummary.StartImport<Program>();
            var res4 = await UpdatePrograms(dbContext, httpClient, cancellationToken);
            programSummary.AddStats(res4);
            programSummary.StopImport();

            await dbContext.Imports.AddAsync(programSummary, cancellationToken);
            
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured during importing");
        }
    }
    
    private async Task<(int received, int added, int updated, int deleted)> UpdateEducationLevels(
        DictionaryDbContext dbContext,
        HttpClient httpClient, 
        CancellationToken cancellationToken)
    {
        var remoteData = await httpClient.GetFromJsonAsync<List<EducationLevelDto>>(
            "https://1c-mockup.kreosoft.space/api/dictionary/education_levels",
            cancellationToken);

        if (remoteData == null) return (0, 0, 0, 0);
        
        var localData = await dbContext.EducationLevels.ToListAsync(cancellationToken);

        int receivedCount = remoteData.Count,
            addedCount = 0,
            updatedCount = 0,
            deletedCount = 0;
        
        var localMap = localData.ToDictionary(x => x.Id);

        foreach (var item in remoteData)
        {
            if (localMap.TryGetValue(item.Id, out var local))
            {
                if (local.Name != item.Name || local.RelevanceStatus != RelevanceStatus.Active)
                {
                    local.Name = item.Name;
                    local.RelevanceStatus = RelevanceStatus.Active;
                    ++updatedCount;
                }

                localMap.Remove(item.Id);
            }
            else
            {
                await dbContext.EducationLevels.AddAsync(
                    new EducationLevel(item.Id, item.Name, RelevanceStatus.Active),
                    cancellationToken);
                ++addedCount;
            }
        }

        foreach (var item in localMap.Values)
        {
            item.RelevanceStatus = RelevanceStatus.Deleted;
            ++deletedCount;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        
        return (receivedCount, addedCount, updatedCount, deletedCount);
    }

    private async Task<(int received, int added, int updated, int deleted)> UpdateDocumentTypes(
        DictionaryDbContext dbContext,
        HttpClient httpClient, 
        CancellationToken cancellationToken)
    {
        var remoteData = await httpClient.GetFromJsonAsync<List<DocumentTypeDto>>(
            "https://1c-mockup.kreosoft.space/api/dictionary/document_types",
            cancellationToken);
        
        if (remoteData == null) return (0, 0, 0, 0);

        var localDocTypes = 
            await dbContext.DocumentTypes
                .Include(d => d.NextEducationLevels)
                .ToListAsync(cancellationToken);
       
        var localEducationLevelsMap = 
            await dbContext.EducationLevels
                .ToDictionaryAsync(e => e.Id, cancellationToken);
        
        int receivedCount = remoteData.Count,
            addedCount = 0,
            updatedCount = 0,
            deletedCount = 0;
        
        var localDocTypesMap = localDocTypes.ToDictionary(x => x.Id);
        
        foreach (var item in remoteData)
        {
            if (localDocTypesMap.TryGetValue(item.Id, out var local))
            {
                bool eduLevelsChanged = 
                    !local.NextEducationLevels
                        .Select(n => n.Id)
                        .OrderBy(id => id)
                        .SequenceEqual(
                            item?.NextEducationLevels
                                .Select(n => n.Id)
                                .OrderBy(id => id));
                
                var remoteEduId = item.EducationLevel?.Id ?? 0;
                
                if (local.RelevanceStatus != RelevanceStatus.Active
                    || local.Name != item.Name
                    || !DateChecker.IsSame(local.CreatedAt, item.CreateTime.ToUtc())
                    || local.EducationLevelId != remoteEduId
                    || eduLevelsChanged)
                {
                    local.Name = item.Name;
                    local.CreatedAt = item.CreateTime.ToUtc();
                    local.EducationLevelId = remoteEduId;
                    local.RelevanceStatus = RelevanceStatus.Active;
                    
                    local.NextEducationLevels.Clear();
                    foreach(var i in item?.NextEducationLevels?.Select(e => e.Id) ?? [])
                        local.NextEducationLevels.Add(localEducationLevelsMap.GetValueOrDefault(i));
                    
                    ++updatedCount;
                }

                localDocTypesMap.Remove(item.Id);
            }
            else
            {
                var newDocType = new DocumentType(
                    item.Id,
                    item.Name,
                    item.CreateTime.ToUtc(),
                    item.EducationLevel?.Id ?? 0,
                    RelevanceStatus.Active);
                
                foreach(var i in item?.NextEducationLevels?.Select(e => e.Id) ?? [])
                    newDocType.NextEducationLevels.Add(localEducationLevelsMap.GetValueOrDefault(i));
                
                await dbContext.DocumentTypes.AddAsync(newDocType, cancellationToken);
                
                ++addedCount;
            }
        }

        foreach (var item in localDocTypesMap.Values)
        {
            item.RelevanceStatus = RelevanceStatus.Deleted;
            ++deletedCount;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return (receivedCount, addedCount, updatedCount, deletedCount);
    }

    private async Task<(int received, int added, int updated, int deleted)> UpdateFaculties(
        DictionaryDbContext dbContext, 
        HttpClient httpClient,
        CancellationToken cancellationToken)
    {
        var remoteData = await httpClient.GetFromJsonAsync<List<FacultyDto>>(
            "https://1c-mockup.kreosoft.space/api/dictionary/faculties",
            cancellationToken);

        if (remoteData == null) return (0, 0, 0, 0);

        var localData = await dbContext.Faculties.ToListAsync(cancellationToken);
        
        int receivedCount = remoteData.Count,
            addedCount = 0,
            updatedCount = 0,
            deletedCount = 0;
        
        var localMap = localData.ToDictionary(x => x.Id);

        foreach (var item in remoteData)
        {
            if (localMap.TryGetValue(item.Id, out var local))
            {
                if (local.Name != item.Name 
                    || !DateChecker.IsSame(local.CreatedAt, item.CreateTime.ToUtc())
                    || local.RelevanceStatus != RelevanceStatus.Active)
                {
                    local.Name = item.Name;
                    local.CreatedAt = item.CreateTime.ToUtc();
                    local.RelevanceStatus = RelevanceStatus.Active;
                    ++updatedCount;
                }

                localMap.Remove(item.Id);
            }
            else
            {
                await dbContext.Faculties.AddAsync(
                    new Faculty(item.Id, item.Name, item.CreateTime.ToUtc(), RelevanceStatus.Active),
                    cancellationToken);
                ++addedCount;
            }
        }

        foreach (var item in localMap.Values)
        {
            item.RelevanceStatus = RelevanceStatus.Deleted;
            ++deletedCount;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        
        return (receivedCount, addedCount, updatedCount, deletedCount);
    }

    private async Task<(int received, int added, int updated, int deleted)> UpdatePrograms(
        DictionaryDbContext dbContext,
        HttpClient httpClient,
        CancellationToken cancellationToken,
        int page = 1,
        int size = 10)
    {
        int receivedCount = 0,
            addedCount = 0,
            updatedCount = 0,
            deletedCount = 0;

        var localData = await dbContext.Programs.ToListAsync(cancellationToken);
        
        var localMap = localData.ToDictionary(x => x.Id);
        
        while (true)
        {
            var remoteRawData = await httpClient.GetFromJsonAsync<KreoProgramResponceDto>(
                $"https://1c-mockup.kreosoft.space/api/dictionary/programs?page={page}&size={size}",
                cancellationToken);

            if (remoteRawData is null || remoteRawData?.Programs is null) break;
            
            var remoteData = remoteRawData?.Programs.ToList();
            
            receivedCount += remoteData.Count;
            
            foreach (var item in remoteData)
            {
                if (localMap.TryGetValue(item.Id, out var local))
                {
                    var remoteFacultyId = item?.Faculty?.Id ?? Guid.Empty;
                    var remoteEducationLevelId = item?.EducationLevel?.Id ?? 0;

                    if (local.Name != item?.Name
                        || local.Code != item.Code
                        || local.Language != item.Language
                        || local.EducationForm != item.EducationForm
                        || !DateChecker.IsSame(local.CreatedAt, item.CreateTime.ToUtc())
                        || local.FacultyId != remoteFacultyId
                        || local.EducationLevelId != remoteEducationLevelId
                        || local.RelevanceStatus != RelevanceStatus.Active)
                    {
                        local.Name = item.Name;
                        local.Code = item.Code;
                        local.Language = item.Language;
                        local.EducationForm = item.EducationForm;
                        local.CreatedAt = item.CreateTime.ToUtc();
                        local.FacultyId = remoteFacultyId;
                        local.EducationLevelId = remoteEducationLevelId;
                        local.RelevanceStatus = RelevanceStatus.Active;

                        ++updatedCount;
                    }

                    localMap.Remove(item.Id);
                }
                else
                {
                    await dbContext.Programs.AddAsync(
                        new Program(
                            item.Id,
                            item.Name,
                            item.Code,
                            item.Language,
                            item.EducationForm,
                            item.CreateTime.ToUtc(),
                            item?.Faculty?.Id ?? Guid.Empty,
                            item?.EducationLevel?.Id ?? 0, RelevanceStatus.Active),
                        cancellationToken);
                    ++addedCount;
                }
            }
            
            page += 1;
            
            if (remoteData.Count < size) break;
        }
        
        foreach (var item in localMap.Values)
        {
            item.RelevanceStatus = RelevanceStatus.Deleted;
            ++deletedCount;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        
        return (receivedCount, addedCount, updatedCount, deletedCount);
    }
}