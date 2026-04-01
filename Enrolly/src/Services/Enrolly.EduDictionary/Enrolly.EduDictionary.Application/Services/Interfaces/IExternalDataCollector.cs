namespace Enrolly.EduDictionary.Application.Services.Interfaces;

public interface IExternalDataCollector
{
    Task ImportAll(CancellationToken cancellationToken = default);
    Task ImportEducationLevels(CancellationToken cancellationToken = default);
    Task ImportDocumentTypes(CancellationToken cancellationToken = default);
    Task ImportFaculties(CancellationToken cancellationToken = default);
    Task ImportPrograms(CancellationToken cancellationToken = default);
}