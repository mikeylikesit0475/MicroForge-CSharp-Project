using MicroForge.Domain.Interfaces;
using MicroForge.Domain.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace MicroForge.Infrastructure.Rag;

/// <summary>
/// Simple bag-of-words embedding service using deterministic hashing.
/// Produces fixed-dimension vectors suitable for cosine similarity search.
/// </summary>
public class EmbeddingService : IEmbeddingService
{
    private readonly int _dimension;

    public EmbeddingService(IOptions<RagSettings> settings)
    {
        _dimension = settings.Value.EmbeddingDimension;
    }

    public Task<float[]> EmbedAsync(string text, CancellationToken ct = default)
    {
        var vector = new float[_dimension];
        var tokens = Tokenize(text);

        foreach (var token in tokens)
        {
            var hash = ComputeHash(token);
            var idx = (int)(hash % (uint)_dimension);
            vector[idx] += 1.0f;
        }

        Normalize(vector);
        return Task.FromResult(vector);
    }

    public async Task<List<float[]>> EmbedBatchAsync(List<string> texts, CancellationToken ct = default)
    {
        var results = new List<float[]>();
        foreach (var text in texts)
        {
            results.Add(await EmbedAsync(text, ct));
        }
        return results;
    }

    public int Dimension => _dimension;

    private static List<string> Tokenize(string text)
    {
        return text.ToLowerInvariant()
            .Split(new[] { ' ', '\t', '\n', '\r', '.', ',', ';', ':', '!', '?', '(', ')', '[', ']', '{', '}', '"', '\'' },
                   StringSplitOptions.RemoveEmptyEntries)
            .Where(t => t.Length > 1)
            .ToList();
    }

    private static uint ComputeHash(string s)
    {
        uint hash = 2166136261u;
        foreach (var c in s)
        {
            hash ^= c;
            hash *= 16777619u;
        }
        return hash;
    }

    private static void Normalize(float[] vector)
    {
        var magnitude = (float)Math.Sqrt(vector.Sum(v => v * v));
        if (magnitude > 0)
        {
            for (var i = 0; i < vector.Length; i++)
                vector[i] /= magnitude;
        }
    }
}