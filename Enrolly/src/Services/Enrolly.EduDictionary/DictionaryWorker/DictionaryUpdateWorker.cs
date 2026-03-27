using System.Net.Http.Json;
using DictionaryWorker.DTOs;
using Enrolly.EduDictionary.Application.Database;
using Enrolly.EduDictionary.Domain.Entities;
using Enrolly.EduDictionary.Domain.Enums;
using Enrolly.EduDictoinary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DictionaryWorker;

public class DictionaryUpdateWorker : BackgroundService
{
    private readonly ILogger<DictionaryUpdateWorker> _logger;
    private readonly TimeSpan _period = new  TimeSpan(0, 8, 0, 0);
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHttpClientFactory _httpClientFactory;
    
    public DictionaryUpdateWorker(
        IServiceScopeFactory scopeFactory,
        ILogger<DictionaryUpdateWorker> logger,
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _scopeFactory = scopeFactory;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<DictionaryDbContext>();
                var httpClient = _httpClientFactory.CreateClient("1c-mockup.kreosoft.client");

                var elSummary = ImportSummary.StartImport<EducationLevel>();
                var res1 = await UpdateEducationLevels(dbContext, httpClient, stoppingToken);
                elSummary.AddStats(res1);
                elSummary.StopImport();
                    
                var dtSummary = ImportSummary.StartImport<DocumentType>();
                var res2 = await UpdateDocumentTypes(dbContext, httpClient, stoppingToken);
                dtSummary.AddStats(res2);
                dtSummary.StopImport();
                    
                var fSummary = ImportSummary.StartImport<Faculty>();
                var res3 = await UpdateFaculties(dbContext, httpClient, stoppingToken);
                fSummary.AddStats(res3);
                fSummary.StopImport();
                    
                //var elSummary = ImportSummary.StartImport<EducationLevel>();
                //var res4 = await UpdatePrograms(dbContext, httpClient, stoppingToken);
                //AddStats(res4);
                
                await dbContext.Imports.AddAsync(elSummary, stoppingToken);
                await dbContext.Imports.AddAsync(elSummary, stoppingToken);
                await dbContext.Imports.AddAsync(elSummary, stoppingToken);
                await dbContext.Imports.AddAsync(elSummary, stoppingToken);
                
                await dbContext.SaveChangesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured during processing");
            }
            
            await Task.Delay(_period, stoppingToken);
        }
    }
    
    private async Task<(int received, int added, int updated, int deleted)> UpdateEducationLevels(
        DictionaryDbContext dbContext,
        HttpClient httpClient, 
        CancellationToken stoppingToken)
    {
        var remoteData = await httpClient.GetFromJsonAsync<List<EducationLevelDto>>(
            "https://1c-mockup.kreosoft.space/api/dictionary/education_levels",
            stoppingToken);

        if (remoteData == null) return (0, 0, 0, 0);
        
        var localData = await dbContext.EducationLevels.ToListAsync(stoppingToken);

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
                    stoppingToken);
                ++addedCount;
            }
        }

        foreach (var item in localMap.Values)
        {
            item.RelevanceStatus = RelevanceStatus.Deleted;
            ++deletedCount;
        }

        await dbContext.SaveChangesAsync(stoppingToken);
        
        return (receivedCount, addedCount, updatedCount, deletedCount);
    }

    private async Task<(int received, int added, int updated, int deleted)> UpdateDocumentTypes(
        DictionaryDbContext dbContext,
        HttpClient httpClient, 
        CancellationToken stoppingToken)
    {
        var remoteData = await httpClient.GetFromJsonAsync<List<DocumentTypeDto>>(
            "https://1c-mockup.kreosoft.space/api/dictionary/document_types",
            stoppingToken);
        
        if (remoteData == null) return (0, 0, 0, 0);

        var localData = 
            await dbContext.DocumentTypes
                .ToListAsync(stoppingToken);
       
        int receivedCount = remoteData.Count,
            addedCount = 0,
            updatedCount = 0,
            deletedCount = 0;
        
        var localMap = localData.ToDictionary(x => x.Id);

        foreach (var item in remoteData)
        {
            if (localMap.TryGetValue(item.Id, out var local))
            {
                var remoteEduId = item.EducationLevel?.Id ?? 0;
                var remoteNextId = item.NextEducationLevel?.Id ?? 0;
                
                if (local.RelevanceStatus != RelevanceStatus.Active
                    || local.Name != item.Name
                    || local.CreatedAt != item.CreatedAt
                    || local.EducationLevelId != remoteEduId
                    || local.NextEducationLevelId != remoteNextId)
                {
                    local.Name = item.Name;
                    local.CreatedAt = item.CreatedAt;
                    local.EducationLevelId = remoteEduId;
                    local.NextEducationLevelId = remoteNextId;
                    local.RelevanceStatus = RelevanceStatus.Active;
                    ++updatedCount;
                }

                localMap.Remove(item.Id);
            }
            else
            {
                await dbContext.DocumentTypes.AddAsync(
                    new DocumentType(
                        item.Id, 
                        item.Name,
                        item.CreatedAt,
                        item.EducationLevel?.Id ?? 0, 
                        item.NextEducationLevel?.Id ?? 0,
                        RelevanceStatus.Active),
                    stoppingToken);
                ++addedCount;
            }
        }

        foreach (var item in localMap.Values)
        {
            item.RelevanceStatus = RelevanceStatus.Deleted;
            ++deletedCount;
        }

        await dbContext.SaveChangesAsync(stoppingToken);

        return (receivedCount, addedCount, updatedCount, deletedCount);
    }

    private async Task<(int received, int added, int updated, int deleted)> UpdateFaculties(
        DictionaryDbContext dbContext, 
        HttpClient httpClient,
        CancellationToken stoppingToken)
    {
        var remoteData = await httpClient.GetFromJsonAsync<List<FacultyDto>>(
            "https://1c-mockup.kreosoft.space/api/dictionary/faculties",
            stoppingToken);

        if (remoteData == null) return (0, 0, 0, 0);

        var localData = await dbContext.Faculties.ToListAsync(stoppingToken);
        
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
                    || local.CreatedAt != item.CreatedAt
                    || local.RelevanceStatus != RelevanceStatus.Active)
                {
                    local.Name = item.Name;
                    local.RelevanceStatus = RelevanceStatus.Active;
                    ++updatedCount;
                }

                localMap.Remove(item.Id);
            }
            else
            {
                await dbContext.Faculties.AddAsync(
                    new Faculty(item.Id, item.Name, item.CreatedAt, RelevanceStatus.Active),
                    stoppingToken);
                ++addedCount;
            }
        }

        foreach (var item in localMap.Values)
        {
            item.RelevanceStatus = RelevanceStatus.Deleted;
            ++deletedCount;
        }

        await dbContext.SaveChangesAsync(stoppingToken);
        
        return (receivedCount, addedCount, updatedCount, deletedCount);
    }

    // предусмотреть пагинацию
    private async Task<(int received, int added, int updated, int deleted)> UpdatePrograms(
        DictionaryDbContext dbContext,
        HttpClient httpClient,
        CancellationToken stoppingToken)
    {
        var remoteData = await httpClient.GetFromJsonAsync<List<ProgramDto>>(
            "https://1c-mockup.kreosoft.space/api/dictionary/programs",
            stoppingToken);

        if (remoteData == null) return (0, 0, 0, 0);

        var localData = await dbContext.Programs.ToListAsync(stoppingToken);
        
        int receivedCount = remoteData.Count,
            addedCount = 0,
            updatedCount = 0,
            deletedCount = 0;
        
        var localMap = localData.ToDictionary(x => x.Id);

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
                    || local.CreatedAt != item.CreatedAt
                    || local.FacultyId != remoteFacultyId
                    || local.EducationLevelId != remoteEducationLevelId
                    || local.RelevanceStatus != RelevanceStatus.Active)
                {
                    local.Name = item.Name;
                    local.Code = item.Code;
                    local.Language = item.Language;
                    local.EducationForm = item.EducationForm;
                    local.CreatedAt = item.CreatedAt;
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
                        item.CreatedAt, 
                        item?.Faculty?.Id ?? Guid.Empty,
                        item?.EducationLevel?.Id ?? 0, RelevanceStatus.Active),
                    stoppingToken);
                ++addedCount;
            }
        }

        foreach (var item in localMap.Values)
        {
            item.RelevanceStatus = RelevanceStatus.Deleted;
            ++deletedCount;
        }

        await dbContext.SaveChangesAsync(stoppingToken);
        
        return (receivedCount, addedCount, updatedCount, deletedCount);
    }
}