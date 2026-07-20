using MicroForge.Application.Dtos;
using MicroForge.Domain.Interfaces;

namespace MicroForge.Application.Services;

public class DocumentAppService : IDocumentAppService
{
    private readonly IDocumentService _documentService;

    public DocumentAppService(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    public async Task<DocumentUploadResponseDto> UploadAsync(Stream stream, string fileName, string contentType, long fileSize, string category, int userId, CancellationToken ct = default)
    {
        var result = await _documentService.UploadAsync(stream, fileName, contentType, fileSize, category, userId, ct);
        return new DocumentUploadResponseDto
        {
            DocumentId = result.DocumentId,
            Title = result.Title,
            ChunkCount = result.ChunkCount,
            Status = result.Status
        };
    }

    public async Task<List<DocumentListDto>> ListAsync(string? category, CancellationToken ct = default)
    {
        var docs = await _documentService.ListAsync(category, ct);
        return docs.Select(d => new DocumentListDto
        {
            Id = d.Id,
            Title = d.Title,
            FileName = d.FileName,
            Category = d.Category,
            FileSize = d.FileSize,
            Status = d.Status,
            UploadedAt = d.UploadedAt
        }).ToList();
    }

    public async Task DeleteAsync(int documentId, CancellationToken ct = default)
    {
        await _documentService.DeleteAsync(documentId, ct);
    }

    public async Task<List<DocumentSearchResultDto>> SearchAsync(string query, int topK, string? category, CancellationToken ct = default)
    {
        var results = await _documentService.SearchAsync(query, topK, category, ct);
        return results.Select(r => new DocumentSearchResultDto
        {
            DocumentId = r.DocumentId,
            ChunkId = r.ChunkId,
            Content = r.Content,
            Similarity = r.Similarity,
            Category = r.Metadata.TryGetValue("category", out var cat) ? cat : ""
        }).ToList();
    }
}