using Enrolly.Documents.Application.Abstractions;
using Enrolly.Documents.Infrastructure.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace Enrolly.Documents.Infrastructure.FileStorage;

public class MinIOStorageService : IFileStorageService
{
    private readonly ILogger<MinIOStorageService> _logger;
    private readonly IMinioClient _minioClient;
    private readonly MinIOSettings _minIOSettings;
    
    public MinIOStorageService(
        IOptions<MinIOSettings> minIoSettings,
        IMinioClient minioClient,
        ILogger<MinIOStorageService> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
        _minIOSettings = minIoSettings.Value;
    }
    
    public async Task<bool> UploadFileAsync(
        string fileName,
        Stream stream,
        string contentType,
        long length,
        string bucketName,
        CancellationToken ct = default)
    {
        stream.Position = 0;

        var args = new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName)
            .WithStreamData(stream)
            .WithObjectSize(length)
            .WithContentType(contentType);

        try
        {
            await _minioClient.PutObjectAsync(args, ct);
        }
        catch (Exception ex)
        {
            await stream.DisposeAsync();
            _logger.LogError("Error while uploading file: {ex}", ex.Message);
            return false;
        }

        return true;
    }

    public async Task<Stream> DownloadFileAsync(
        string fileName,
        string bucketName,
        CancellationToken ct = default)
    {
        var stream = new MemoryStream();

        var args = new GetObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName)
            .WithCallbackStream(s => s.CopyTo(stream));
        try
        {
            await _minioClient.GetObjectAsync(args, ct);
            stream.Position = 0;
            return stream;
        }
        catch (Exception ex) 
        {
            await stream.DisposeAsync();
            _logger.LogError("Error while downloading file: {ex}", ex.Message);
            throw;
        }
    }

    public Task<string> GetPresignedUriAsync(string uri, string bucketName, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteFileAsync(
        string fileName,
        string bucketName,
        CancellationToken ct = default)
    {
        try
        {
            var deleteArgs = new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName);

            await _minioClient.RemoveObjectAsync(deleteArgs, ct);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to delete file from MinIO: {message}", ex.Message);
            return false;
        }
    }
}