using System.Net.Http.Json;
using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Shared.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Enrolly.Admissions.Infrastructure.Workers;

public class DictionarySyncWorker : BackgroundService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<DictionarySyncWorker> _logger;

    public DictionarySyncWorker(
        IHttpClientFactory httpClientFactory,
        IServiceScopeFactory scopeFactory,
        ILogger<DictionarySyncWorker> logger)
    {
        _httpClientFactory = httpClientFactory;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("DictionarySyncWorker started");

        try
        {
            await SyncAllAsync(stoppingToken);
            _logger.LogInformation("DictionarySyncWorker completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DictionarySyncWorker failed");
            throw;
        }
    }

    private async Task SyncAllAsync(CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();

        var facultyRepo = scope.ServiceProvider.GetRequiredService<IFacultyRepository>();
        var educationLevelRepo = scope.ServiceProvider.GetRequiredService<IEducationLevelRepository>();
        var documentTypeRepo = scope.ServiceProvider.GetRequiredService<IEducationDocumentTypeRepository>();
        var programRepo = scope.ServiceProvider.GetRequiredService<IProgramRepository>();

        var client = _httpClientFactory.CreateClient("DictionaryClient");

        await SyncEducationLevelsAsync(client, educationLevelRepo, ct);
        await SyncFacultiesAsync(client, facultyRepo, ct);
        await SyncDocumentTypesAsync(client, documentTypeRepo, ct);
        await SyncProgramsAsync(client, programRepo, ct);
    }

    private async Task SyncEducationLevelsAsync(
        HttpClient client,
        IEducationLevelRepository repo,
        CancellationToken ct)
    {
        _logger.LogInformation("Syncing education levels...");

        var dtos = await client.GetFromJsonAsync<List<EducationLevel>>(
            "api/v1/dictionary/edulevels", ct) ?? [];

        foreach (var dto in dtos)
        {
            var entity = new EducationLevel { Id = dto.Id, Name = dto.Name };

            var existing = await repo.GetById(dto.Id);

            if (existing.IsSuccess)
                await repo.Update(entity);
            else
                await repo.Add(entity);
        }

        _logger.LogInformation("Education levels synced: {Count}", dtos.Count);
    }

    private async Task SyncFacultiesAsync(
        HttpClient client,
        IFacultyRepository repo,
        CancellationToken ct)
    {
        _logger.LogInformation("Syncing faculties...");

        var dtos = await client.GetFromJsonAsync<List<Faculty>>(
            "api/v1/dictionary/faculties", ct) ?? [];

        foreach (var dto in dtos)
        {
            var entity = new Faculty
            {
                Id = dto.Id,
                CreateTime = dto.CreateTime,
                Name = dto.Name
            };

            var existing = await repo.GetById(dto.Id);

            if (existing.IsSuccess)
                await repo.Update(entity);
            else
                await repo.Add(entity);
        }

        _logger.LogInformation("Faculties synced: {Count}", dtos.Count);
    }
    
    private async Task SyncDocumentTypesAsync(
        HttpClient client,
        IEducationDocumentTypeRepository repo,
        CancellationToken ct)
    {
        _logger.LogInformation("Syncing document types...");

        var dtos = await client.GetFromJsonAsync<List<EducationDocumentType>>(
            "api/v1/dictionary/doctypes", ct) ?? [];

        foreach (var dto in dtos)
        {
            var entity = new EducationDocumentType
            {
                Id = dto.Id,
                CreateTime = dto.CreateTime,
                Name = dto.Name,
                EducationLevelId = dto.EducationLevel?.Id ?? 0,
                NextEducationLevelIds = dto.NextEducationLevels?
                    .Select(l => l.Id)
                    .ToList() ?? []
            };

            var existing = await repo.GetById(dto.Id);

            if (existing.IsSuccess)
                await repo.Update(entity);
            else
                await repo.Add(entity);
        }

        _logger.LogInformation("Document types synced: {Count}", dtos.Count);
    }

    private async Task SyncProgramsAsync(
        HttpClient client,
        IProgramRepository repo,
        CancellationToken ct)
    {
        _logger.LogInformation("Syncing programs...");

        const int pageSize = 100;
        var page = 1;
        var totalSynced = 0;

        while (!ct.IsCancellationRequested)
        {
            var pagedDtos = await client.GetFromJsonAsync<PagedResponce<Program>>(
                $"api/v1/dictionary/programs?page={page}&pageSize={pageSize}", ct);

            var dtos = pagedDtos.Content.ToList();
            
            if (dtos.Count == 0)
                break;

            foreach (var dto in dtos)
            {
                var entity = new Program
                {
                    Id = dto.Id,
                    CreateTime = dto.CreateTime,
                    Name = dto.Name,
                    Code = dto.Code,
                    Language = dto.Language,
                    EducationForm = dto.EducationForm,
                    FacultyId = dto.Faculty?.Id,
                    EducationLevelId = dto.EducationLevel?.Id
                };

                var existing = await repo.GetById(dto.Id);

                if (existing.IsSuccess)
                    await repo.Update(entity);
                else
                    await repo.Add(entity);
            }

            totalSynced += dtos.Count;
            _logger.LogDebug("Programs page {Page} synced ({Count} items)", page, dtos.Count);

            if (dtos.Count < pageSize)
                break;

            page++;
        }

        _logger.LogInformation("Programs synced: {Total}", totalSynced);
    }
}