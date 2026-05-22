using Enrolly.Documents.Application.Abstractions;
using Enrolly.Documents.Application.Abstractions.Services;
using Enrolly.Documents.Domain.Repositories;
using File = Enrolly.Documents.Domain.Entities.File;

namespace Enrolly.Documents.Application.Services;

public class EducationDocumentScansService : IEducationDocumentScansService
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IScanRepository _scanRepository;
    private readonly string _bucketName = "enrolly-documents-diplomas";
    
    public EducationDocumentScansService(IFileStorageService fileStorageService, IScanRepository scanRepository)
    {
        _fileStorageService = fileStorageService;
        _scanRepository = scanRepository;
    }

    public async Task<Guid> UploadScan(
        Guid userId, Guid documentId, 
        string originalFileName, 
        Stream stream, 
        string contentType,
        CancellationToken cancellationToken = default)
    {
        File scan = new File(userId, documentId, originalFileName, Path.GetExtension(originalFileName));
        scan.ContentType = contentType;
        scan.Url = $"{scan.UserId}/{scan.EducationDocumentId}/scans/{scan.Id}{scan.Extension}";
        
        await _fileStorageService.UploadFileAsync(scan.Url, stream, contentType, stream.Length, _bucketName, cancellationToken);
        
        await _scanRepository.AddScanAsync(scan);
        
        return scan.Id;
    }

    public async Task<(Stream stream, string contentType, string fileName)> DownloadScan(
        Guid userId,
        Guid documentId,
        Guid scanId,
        CancellationToken cancellationToken = default)
    {
        var file = await _scanRepository.GetByIdAsync(scanId);
        
        var stream = await _fileStorageService.DownloadFileAsync(file.Url, _bucketName, cancellationToken);
        return (stream, file.ContentType, file.OriginalName);
    }

    public async Task DeleteScan(
        Guid userId,
        Guid documentId,
        Guid scanId,
        CancellationToken cancellationToken = default)
    {
        var scan =  await _scanRepository.GetByIdAsync(scanId);
        
        await _fileStorageService.DeleteFileAsync(scan.Url, _bucketName, cancellationToken);
        
        await _scanRepository.RemoveScanAsync(scanId);
    }
}