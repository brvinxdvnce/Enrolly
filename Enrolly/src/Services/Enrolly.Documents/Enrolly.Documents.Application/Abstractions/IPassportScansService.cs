namespace Enrolly.Documents.Application.Abstractions;

public interface IPassportScansService
{
    public Task<Guid> UploadScan(Guid userId, string originalFileName, Stream stream, string contentType, CancellationToken cancellationToken = default);
    public Task<(Stream stream, string contentType, string fileName)> DownloadScan(Guid userId, Guid scanId, CancellationToken cancellationToken = default);
    public Task DeleteScan(Guid userId, Guid scanId, CancellationToken cancellationToken = default);

}