using Enrolly.Admissions.Application.Abstractions.Services;
using CSharpFunctionalExtensions;
using Enrolly.Admissions.Application.Settings;
using Enrolly.Admissions.Domain.Enums;
using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Shared.Logging.Utils.Result;
using Microsoft.Extensions.Options;

namespace Enrolly.Admissions.Application.Services;

public class AdmissionProgramService : IAdmissionProgramService
{
    private readonly IAdmissionRepository _admissionRepository;
    private readonly IAdmissionProgramRepository _admissionProgramRepository;
    private readonly AdmissionSettings _admissionSettings;
    
    public AdmissionProgramService(
        IAdmissionProgramRepository admissionProgramRepository,
        IAdmissionRepository admissionRepository,
        IOptions<AdmissionSettings> admissionSettings)
    {
        _admissionProgramRepository = admissionProgramRepository;
        _admissionRepository = admissionRepository;
        _admissionSettings = admissionSettings.Value;
    }

    public async Task<Result> GetAdmissionPrograms(Guid admissionId)
    {
        return await _admissionRepository.GetById(admissionId);
    }

    public async Task<Result> AddProgramToAdmission(Guid admissionId, Guid programId, int programPriority = 1)
    {
        return await _admissionRepository.GetById(admissionId)
            .Ensure(admission => admission.AdmissionStatus != AdmissionStatus.Closed,
                ResultError.Forbidden())
            .Ensure(admission => admission.Programs.Count() < _admissionSettings.MaxProgramsPerAdmission,
                ResultError.Conflict("The maximum number of programs in the admission has been reached"))
            .Bind(async admission =>
                await _admissionProgramRepository.Add(
                    admissionId,
                    programId,
                    CalculateOptimalPriority(
                        programPriority,
                        _admissionSettings.MaxProgramsPerAdmission,
                        admission.Programs.Select(p => p.Priority).ToList()
                        )));
    }

    public async Task<Result> RemoveProgramFromAdmission(Guid admissionId, Guid programId)
    {
        return await _admissionRepository.GetById(admissionId)
            .Ensure(admission => admission.AdmissionStatus != AdmissionStatus.Closed,
                ResultError.Forbidden())
            .Bind(async _ =>
                await _admissionProgramRepository.RemoveById(admissionId, programId));
    }

    public async Task<Result> ChangeProgramPriority(Guid admissionId, Guid programId, int newPriority)
    {
        return await _admissionRepository.GetById(admissionId)
            .Ensure(admission => admission.AdmissionStatus != AdmissionStatus.Closed,
                ResultError.Forbidden())
            .Bind(async admission =>
                await _admissionProgramRepository.ChangeProgramPriority(
                    admissionId,
                    programId, 
                    CalculateOptimalPriority(
                        newPriority,
                        _admissionSettings.MaxProgramsPerAdmission,
                        admission.Programs.Select(p => p.Priority).ToList()
                        )));
    }

    private int ValidatePriorityRange(int currentPriority, int limit)
    {
        if (0 < currentPriority && currentPriority <= limit)
            return currentPriority;
        return 1;
    }

    private int CalculateOptimalPriority(int currentPriority, int limit, IReadOnlyCollection<int> priorities)
    {
        
        var targetPriority = ValidatePriorityRange(currentPriority, limit);

        if (priorities.Count == 0)
            return targetPriority;

        if (!priorities.Contains(targetPriority))
            return targetPriority;
        
        var availablePriorities = Enumerable
            .Range(1, limit)
            .Except(priorities).ToList();

        if (availablePriorities.Count == 0) return targetPriority;
        
        return availablePriorities.Min();
    }
}