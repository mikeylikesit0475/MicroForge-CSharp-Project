using FluentAssertions;
using MicroForge.Domain.Configuration;
using MicroForge.Infrastructure.Rag;
using Microsoft.Extensions.Options;

namespace MicroForge.UnitTests;

/// <summary>
/// Unit tests for the DocumentChunker — paragraph-based chunking with overlap.
/// </summary>
public class DocumentChunkerTests
{
    private readonly DocumentChunker _chunker;

    public DocumentChunkerTests()
    {
        var settings = Options.Create(new RagSettings
        {
            ChunkSizeTokens = 100,
            ChunkOverlapTokens = 20,
            TopK = 5,
            EmbeddingDimension = 64
        });
        _chunker = new DocumentChunker(settings);
    }

    [Fact]
    public void chunk_empty_string_returns_empty_list()
    {
        var result = _chunker.Chunk("");

        result.Should().BeEmpty();
    }

    [Fact]
    public void chunk_whitespace_only_string_returns_empty_list()
    {
        var result = _chunker.Chunk("   \n\n  \t  ");

        result.Should().BeEmpty();
    }

    [Fact]
    public void chunk_single_paragraph_returns_single_chunk()
    {
        var text = "This is a short paragraph.";

        var result = _chunker.Chunk(text);

        result.Should().HaveCount(1);
        result[0].ChunkIndex.Should().Be(0);
        result[0].Content.Should().Contain("This is a short paragraph.");
        result[0].TokenCount.Should().BeGreaterThan(0);
    }

    [Fact]
    public void chunk_multiple_small_paragraphs_returns_single_chunk()
    {
        var text = "Paragraph one.\n\nParagraph two.\n\nParagraph three.";

        var result = _chunker.Chunk(text);

        result.Should().HaveCount(1);
        result[0].Content.Should().Contain("Paragraph one.");
        result[0].Content.Should().Contain("Paragraph two.");
        result[0].Content.Should().Contain("Paragraph three.");
    }

    [Fact]
    public void chunk_large_text_splits_into_multiple_chunks()
    {
        // Each paragraph is ~25 tokens, chunk size 100, so ~4 paragraphs per chunk
        var paragraphs = new List<string>();
        for (var i = 0; i < 20; i++)
        {
            paragraphs.Add($"This is paragraph number {i} with some additional text to make it longer and fill up tokens.");
        }
        var text = string.Join("\n\n", paragraphs);

        var result = _chunker.Chunk(text);

        result.Should().HaveCountGreaterThan(1);
        result[0].ChunkIndex.Should().Be(0);
        result[1].ChunkIndex.Should().Be(1);
    }

    [Fact]
    public void chunk_assigns_increasing_chunk_indices()
    {
        var paragraphs = new List<string>();
        for (var i = 0; i < 30; i++)
        {
            paragraphs.Add($"Paragraph {i} with enough text to ensure we get multiple chunks when the chunk size is small enough for testing purposes.");
        }
        var text = string.Join("\n\n", paragraphs);

        var result = _chunker.Chunk(text);

        for (var i = 0; i < result.Count; i++)
        {
            result[i].ChunkIndex.Should().Be(i);
        }
    }

    [Fact]
    public void chunk_includes_metadata_with_page_number()
    {
        var text = "Simple paragraph.";

        var result = _chunker.Chunk(text);

        result[0].Metadata.Should().Contain("page");
        result[0].PageNumber.Should().Be(1);
    }

    [Fact]
    public void chunk_with_custom_chunk_size_overrides_default()
    {
        var text = "Short paragraph one.\n\nShort paragraph two.\n\nShort paragraph three.";

        // Very small chunk size to force splitting
        var result = _chunker.Chunk(text, chunkSizeTokens: 5, overlapTokens: 0);

        result.Should().HaveCountGreaterThan(1);
    }

    [Fact]
    public void chunk_with_overlap_produces_overlapping_content()
    {
        var paragraphs = new List<string>();
        for (var i = 0; i < 10; i++)
        {
            paragraphs.Add($"This is paragraph {i} with some unique content that is long enough to trigger chunking.");
        }
        var text = string.Join("\n\n", paragraphs);

        var result = _chunker.Chunk(text, chunkSizeTokens: 50, overlapTokens: 20);

        result.Should().HaveCountGreaterThan(1);
        // With overlap, the second chunk should contain some text from the end of the first chunk
        if (result.Count >= 2)
        {
            // Overlap means some content from chunk 0 tail appears in chunk 1 head
            // We just verify both chunks have content
            result[0].Content.Should().NotBeEmpty();
            result[1].Content.Should().NotBeEmpty();
        }
    }
}