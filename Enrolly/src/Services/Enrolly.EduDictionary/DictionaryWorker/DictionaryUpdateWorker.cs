using System.Net.Http.Json;
using DictionaryWorker.DTOs;
using Enrolly.EduDictionary.Application.Database;
using Enrolly.EduDictionary.Application.Services.Interfaces;
using Enrolly.EduDictionary.Domain.Entities;
using Enrolly.EduDictionary.Domain.Enums;
using Enrolly.EduDictoinary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Enrolly.Shared.Logging;
using Microsoft.EntityFrameworkCore.Storage;

namespace DictionaryWorker;

public class DictionaryUpdateWorker : BackgroundService
{
    private readonly ILogger<DictionaryUpdateWorker> _logger;
    private readonly TimeSpan _period = new  TimeSpan(0, 8, 0, 0);
    private readonly IServiceScopeFactory _scopeFactory;
    
    public DictionaryUpdateWorker(
        IServiceScopeFactory scopeFactory,
        ILogger<DictionaryUpdateWorker> logger)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }
    
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                
                var dataCollector = scope.ServiceProvider.GetRequiredService<IExternalDataCollector>();
                
                _logger.LogInformation("Starting planned dictionary update");
                
                await dataCollector.ImportAll(cancellationToken);
                
                _logger.LogInformation("Finished planned dictionary update");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured during processing");
            }
            
            await Task.Delay(_period, cancellationToken);
        }
    }
}