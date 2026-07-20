using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MicroForge.Infrastructure.Rag;

public class DocumentService : IDocumentService
{
    private readonly MicroForgeDbContext _db;
    private readonly IDocumentChunker _chunker;
    private readonly IEmbeddingService _embedder;
    private readonly IVectorStore _vectorStore;
    private readonly ILogger<DocumentService> _logger;

    public DocumentService(
        MicroForgeDbContext db,
        IDocumentChunker chunker,
        IEmbeddingService embedder,
        IVectorStore vectorStore,
        ILogger<DocumentService> logger)
    {
        _db = db;
        _chunker = chunker;
        _embedder = embedder;
        _vectorStore = vectorStore;
        _logger = logger;
    }

    public async Task<DocumentUploadResult> UploadAsync(Stream stream, string fileName, string contentType, long fileSize, string category, int userId, CancellationToken ct = default)
    {
        using var reader = new StreamReader(stream);
        var text = await reader.ReadToEndAsync(ct);
        var title = Path.GetFileNameWithoutExtension(fileName);

        var doc = new Document
        {
            Title = title,
            FileName = fileName,
            ContentType = contentType,
            FileSize = fileSize,
            Category = category,
            UploadedByUserId = userId,
            UploadedAt = DateTime.UtcNow,
            Status = "processing"
        };
        _db.Documents.Add(doc);
        await _db.SaveChangesAsync(ct);

        var chunks = _chunker.Chunk(text);
        foreach (var chunk in chunks)
        {
            var embedding = await _embedder.EmbedAsync(chunk.Content, ct);
            var embeddingJson = JsonSerializer.Serialize(embedding);

            var docChunk = new DocumentChunk
            {
                DocumentId = doc.Id,
                ChunkIndex = chunk.ChunkIndex,
                Content = chunk.Content,
                Embedding = embeddingJson,
                TokenCount = chunk.TokenCount,
                PageNumber = chunk.PageNumber,
                Metadata = chunk.Metadata
            };
            _db.DocumentChunks.Add(docChunk);

            var metadata = new Dictionary<string, string>
            {
                ["category"] = category,
                ["title"] = title,
                ["fileName"] = fileName
            };
            await _vectorStore.AddAsync(doc.Id, chunk.ChunkIndex, embedding, chunk.Content, metadata, ct);
        }

        doc.Status = "processed";
        await _db.SaveChangesAsync(ct);

        _logger.LogInformation("Document {Title} uploaded with {ChunkCount} chunks", title, chunks.Count);

        return new DocumentUploadResult
        {
            DocumentId = doc.Id,
            Title = title,
            ChunkCount = chunks.Count,
            Status = "Processed"
        };
    }

    public async Task<List<DocumentListItem>> ListAsync(string? category, CancellationToken ct = default)
    {
        var query = _db.Documents.AsQueryable();
        if (!string.IsNullOrEmpty(category))
            query = query.Where(d => d.Category == category);
        var docs = await query.OrderByDescending(d => d.UploadedAt).ToListAsync(ct);
        return docs.Select(d => new DocumentListItem
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
        var doc = await _db.Documents.FirstOrDefaultAsync(d => d.Id == documentId, ct);
        if (doc == null) return;

        var chunks = await _db.DocumentChunks.Where(c => c.DocumentId == documentId).ToListAsync(ct);
        _db.DocumentChunks.RemoveRange(chunks);
        _db.Documents.Remove(doc);
        await _db.SaveChangesAsync(ct);
        await _vectorStore.DeleteByDocumentAsync(documentId, ct);

        _logger.LogInformation("Document {DocumentId} deleted", documentId);
    }

    public async Task<List<VectorSearchResult>> SearchAsync(string query, int topK, string? category, CancellationToken ct = default)
    {
        var queryEmbedding = await _embedder.EmbedAsync(query, ct);
        return await _vectorStore.SearchAsync(queryEmbedding, topK, category, ct);
    }
}