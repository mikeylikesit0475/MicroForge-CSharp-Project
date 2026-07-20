namespace MicroForge.Domain.Entities;

/// <summary>
/// Represents a chunked and embedded fragment of a document for RAG.
/// </summary>
public class DocumentChunk
{
    public int Id { get; set; }
    public int DocumentId { get; set; }
    public Document? Document { get; set; }
    public int ChunkIndex { get; set; }
    public string Content { get; set; } = string.Empty;
    public int TokenCount { get; set; }
    public int PageNumber { get; set; }
    public string Embedding { get; set; } = string.Empty;
    public string Metadata { get; set; } = string.Empty;
}