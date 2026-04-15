namespace Enrolly.Documents.Application.Abstractions;

public interface IDocumentScansService
{
    public Task<Guid> UploadScan(Guid userId, Guid documentId, string originalFileName, Stream stream, string contentType, CancellationToken cancellationToken = default);
    public Task<(Stream stream, string contentType, string fileName)> DownloadScan(Guid userId, Guid documentId, Guid scanId, CancellationToken cancellationToken = default);
    public Task DeleteScan(Guid userId, Guid documentId, Guid scanId, CancellationToken cancellationToken = default);

}