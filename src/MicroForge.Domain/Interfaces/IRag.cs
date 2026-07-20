namespace MicroForge.Domain.Interfaces;

/// <summary>
/// Splits documents into overlapping chunks for RAG embedding.
/// </summary>
public interface IDocumentChunker
{
    List<DocumentChunkResult> Chunk(string text, int? chunkSizeTokens = null, int? overlapTokens = null);
}

/// <summary>
/// A chunk result from the document chunker.
/// </summary>
public class DocumentChunkResult
{
    public int ChunkIndex { get; init; }
    public string Content { get; init; } = string.Empty;
    public int TokenCount { get; init; }
    public int PageNumber { get; init; }
    public string Metadata { get; init; } = string.Empty;
}

/// <summary>
/// Generates vector embeddings for text.
/// </summary>
public interface IEmbeddingService
{
    Task<float[]> EmbedAsync(string text, CancellationToken ct = default);
    Task<List<float[]>> EmbedBatchAsync(List<string> texts, CancellationToken ct = default);
    int Dimension { get; }
}

/// <summary>
/// Vector store for semantic search with cosine similarity.
/// </summary>
public interface IVectorStore
{
    Task AddAsync(int documentId, int chunkId, float[] embedding, string content, Dictionary<string, string> metadata, CancellationToken ct = default);
    Task<List<VectorSearchResult>> SearchAsync(float[] queryEmbedding, int topK, string? categoryFilter = null, CancellationToken ct = default);
    Task DeleteByDocumentAsync(int documentId, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
}

/// <summary>
/// A search result from the vector store.
/// </summary>
public class VectorSearchResult
{
    public int DocumentId { get; init; }
    public int ChunkId { get; init; }
    public string Content { get; init; } = string.Empty;
    public double Similarity { get; init; }
    public Dictionary<string, string> Metadata { get; init; } = new();
}

/// <summary>
/// Extracts text from documents (PDF, TXT, MD).
/// </summary>
public interface ITextExtractor
{
    bool Supports(string contentType);
    Task<string> ExtractAsync(Stream stream, string contentType, CancellationToken ct = default);
}

/// <summary>
/// Orchestrates the document ingestion pipeline.
/// </summary>
public interface IDocumentService
{
    Task<DocumentUploadResult> UploadAsync(Stream stream, string fileName, string contentType, long fileSize, string category, int userId, CancellationToken ct = default);
    Task<List<DocumentListItem>> ListAsync(string? category = null, CancellationToken ct = default);
    Task DeleteAsync(int documentId, CancellationToken ct = default);
    Task<List<VectorSearchResult>> SearchAsync(string query, int topK = 5, string? category = null, CancellationToken ct = default);
}

/// <summary>
/// Result of a document upload.
/// </summary>
public class DocumentUploadResult
{
    public int DocumentId { get; init; }
    public string Title { get; init; } = string.Empty;
    public int ChunkCount { get; init; }
    public string Status { get; init; } = string.Empty;
}

/// <summary>
/// A document list item.
/// </summary>
public class DocumentListItem
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string FileName { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public long FileSize { get; init; }
    public string Status { get; init; } = string.Empty;
    public DateTime UploadedAt { get; init; }
}