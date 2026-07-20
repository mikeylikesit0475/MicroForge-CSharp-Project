using FluentAssertions;
using MicroForge.Domain.Configuration;
using MicroForge.Infrastructure.Rag;
using Microsoft.Extensions.Options;

namespace MicroForge.UnitTests;

/// <summary>
/// Unit tests for the EmbeddingService — deterministic bag-of-words hashing embeddings.
/// </summary>
public class EmbeddingServiceTests
{
    private readonly EmbeddingService _embedder;

    public EmbeddingServiceTests()
    {
        var settings = Options.Create(new RagSettings
        {
            ChunkSizeTokens = 100,
            ChunkOverlapTokens = 20,
            TopK = 5,
            EmbeddingDimension = 128
        });
        _embedder = new EmbeddingService(settings);
    }

    [Fact]
    public async Task embedasync_returns_vector_of_configured_dimension()
    {
        var result = await _embedder.EmbedAsync("hello world");

        result.Should().HaveCount(128);
    }

    [Fact]
    public async Task embedasync_same_text_produces_same_embedding()
    {
        var text = "manufacturing quality control process";

        var result1 = await _embedder.EmbedAsync(text);
        var result2 = await _embedder.EmbedAsync(text);

        result1.Should().Equal(result2);
    }

    [Fact]
    public async Task embedasync_different_text_produces_different_embedding()
    {
        var result1 = await _embedder.EmbedAsync("temperature sensor reading");
        var result2 = await _embedder.EmbedAsync("pressure gauge measurement");

        result1.Should().NotEqual(result2);
    }

    [Fact]
    public async Task embedasync_empty_string_returns_zero_vector()
    {
        var result = await _embedder.EmbedAsync("");

        result.Should().HaveCount(128);
        result.All(v => v == 0f).Should().BeTrue();
    }

    [Fact]
    public async Task embedasync_is_case_insensitive()
    {
        var result1 = await _embedder.EmbedAsync("Hello World");
        var result2 = await _embedder.EmbedAsync("hello world");

        result1.Should().Equal(result2);
    }

    [Fact]
    public async Task embedasync_produces_normalized_vector()
    {
        var result = await _embedder.EmbedAsync("normalization test for vector magnitude");

        var magnitude = Math.Sqrt(result.Sum(v => (double)v * v));
        magnitude.Should().BeApproximately(1.0, 0.001);
    }

    [Fact]
    public async Task embedbatchasync_returns_embeddings_for_all_texts()
    {
        var texts = new List<string> { "first text", "second text", "third text" };

        var results = await _embedder.EmbedBatchAsync(texts);

        results.Should().HaveCount(3);
        results[0].Should().HaveCount(128);
        results[1].Should().HaveCount(128);
        results[2].Should().HaveCount(128);
    }

    [Fact]
    public async Task embedbatchasync_empty_list_returns_empty_list()
    {
        var results = await _embedder.EmbedBatchAsync(new List<string>());

        results.Should().BeEmpty();
    }

    [Fact]
    public void dimension_returns_configured_value()
    {
        _embedder.Dimension.Should().Be(128);
    }

    [Fact]
    public async Task embedasync_similar_text_produces_higher_similarity_than_dissimilar()
    {
        var embedding1 = await _embedder.EmbedAsync("temperature sensor anomaly");
        var embedding2 = await _embedder.EmbedAsync("temperature sensor reading");
        var embedding3 = await _embedder.EmbedAsync("completely different topic about cooking");

        var simsimilar = cosinesimilarity(embedding1, embedding2);
        var simdifferent = cosinesimilarity(embedding1, embedding3);

        simsimilar.Should().BeGreaterThan(simdifferent);
    }

    private static double cosinesimilarity(float[] a, float[] b)
    {
        var dot = 0.0;
        var maga = 0.0;
        var magb = 0.0;
        for (var i = 0; i < a.Length; i++)
        {
            dot += a[i] * b[i];
            maga += a[i] * a[i];
            magb += b[i] * b[i];
        }
        var denom = Math.Sqrt(maga) * Math.Sqrt(magb);
        return denom > 0 ? dot / denom : 0;
    }
}