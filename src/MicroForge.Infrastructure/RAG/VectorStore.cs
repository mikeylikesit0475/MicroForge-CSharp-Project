using MicroForge.Domain.Interfaces;
using System.Collections.Concurrent;

namespace MicroForge.Infrastructure.Rag;

/// <summary>
/// In-memory vector store with brute-force cosine similarity search.
/// Thread-safe, suitable for development and small document sets.
/// </summary>
public class VectorStore : IVectorStore
{
    private readonly ConcurrentDictionary<(int docId, int chunkId), VectorEntry> _entries = new();
    private int _counter;

    private class VectorEntry
    {
        public int DocumentId { get; init; }
        public int ChunkId { get; init; }
        public float[] Embedding { get; init; } = Array.Empty<float>();
        public string Content { get; init; } = "";
        public Dictionary<string, string> Metadata { get; init; } = new();
    }

    public Task AddAsync(int documentId, int chunkId, float[] embedding, string content, Dictionary<string, string> metadata, CancellationToken ct = default)
    {
        var entry = new VectorEntry
        {
            DocumentId = documentId,
            ChunkId = chunkId,
            Embedding = embedding,
            Content = content,
            Metadata = metadata
        };
        _entries[(documentId, chunkId)] = entry;
        Interlocked.Increment(ref _counter);
        return Task.CompletedTask;
    }

    public Task<List<VectorSearchResult>> SearchAsync(float[] queryEmbedding, int topK, string? categoryFilter = null, CancellationToken ct = default)
    {
        var results = new List<VectorSearchResult>();

        foreach (var entry in _entries.Values)
        {
            if (categoryFilter != null && entry.Metadata.TryGetValue("category", out var cat) && cat != categoryFilter)
                continue;

            var similarity = CosineSimilarity(queryEmbedding, entry.Embedding);
            results.Add(new VectorSearchResult
            {
                DocumentId = entry.DocumentId,
                ChunkId = entry.ChunkId,
                Content = entry.Content,
                Similarity = similarity,
                Metadata = entry.Metadata
            });
        }

        return Task.FromResult(results.OrderByDescending(r => r.Similarity).Take(topK).ToList());
    }

    public Task DeleteByDocumentAsync(int documentId, CancellationToken ct = default)
    {
        var keys = _entries.Where(kvp => kvp.Key.docId == documentId).Select(kvp => kvp.Key).ToList();
        foreach (var key in keys)
            _entries.TryRemove(key, out _);
        return Task.CompletedTask;
    }

    public Task<int> CountAsync(CancellationToken ct = default)
    {
        return Task.FromResult(_entries.Count);
    }

    private static double CosineSimilarity(float[] a, float[] b)
    {
        if (a.Length != b.Length) return 0;
        var dot = 0.0;
        var magA = 0.0;
        var magB = 0.0;
        for (var i = 0; i < a.Length; i++)
        {
            dot += a[i] * b[i];
            magA += a[i] * a[i];
            magB += b[i] * b[i];
        }
        var denom = Math.Sqrt(magA) * Math.Sqrt(magB);
        return denom > 0 ? dot / denom : 0;
    }
}