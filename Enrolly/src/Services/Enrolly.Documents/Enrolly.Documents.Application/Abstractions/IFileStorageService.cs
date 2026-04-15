using Microsoft.AspNetCore.Http;

namespace Enrolly.Documents.Application.Abstractions;

public interface IFileStorageService
{
    public Task<bool> UploadFileAsync(string fileName, Stream stream, string contentType, long length, string bucketName, CancellationToken ct = default);
    public Task<Stream> DownloadFileAsync(string fileName, string bucketName, CancellationToken ct = default);
    public Task<string> GetPresignedUriAsync(string uri, string bucketName, CancellationToken ct = default);
    public Task<bool> DeleteFileAsync(string fileName, string bucketName, CancellationToken ct = default);
}