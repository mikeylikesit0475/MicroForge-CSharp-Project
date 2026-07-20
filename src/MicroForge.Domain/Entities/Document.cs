namespace MicroForge.Domain.Entities;

/// <summary>
/// Represents an uploaded manufacturing document (SOP, runbook, spec).
/// </summary>
public class Document
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string Category { get; set; } = string.Empty;
    public int UploadedByUserId { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "processing";
    public string? ErrorMessage { get; set; }

    public ICollection<DocumentChunk> DocumentChunks { get; set; } = new List<DocumentChunk>();
}