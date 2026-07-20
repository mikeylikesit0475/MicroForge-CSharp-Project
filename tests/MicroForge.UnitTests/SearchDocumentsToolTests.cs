using FluentAssertions;
using MicroForge.Domain.Configuration;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.AI.Tools;
using MicroForge.Infrastructure.Rag;
using Microsoft.Extensions.Options;
using System.Text.Json.Nodes;

namespace MicroForge.UnitTests;

/// <summary>
/// Unit tests for the SearchDocumentsTool — RAG-powered semantic document search.
/// </summary>
public class SearchDocumentsToolTests : TestBase
{
    private IDocumentService createdocumentservice()
    {
        var chunker = new DocumentChunker(Options.Create(new RagSettings
        {
            ChunkSizeTokens = 100,
            ChunkOverlapTokens = 20,
            TopK = 5,
            EmbeddingDimension = 64
        }));
        var embedder = new EmbeddingService(Options.Create(new RagSettings
        {
            ChunkSizeTokens = 100,
            ChunkOverlapTokens = 20,
            TopK = 5,
            EmbeddingDimension = 64
        }));
        var vectorStore = new VectorStore();
        return new DocumentService(DbContext, chunker, embedder, vectorStore,
            Microsoft.Extensions.Logging.Abstractions.NullLogger<DocumentService>.Instance);
    }

    [Fact]
    public async Task invokeasync_with_no_documents_returns_zero_count()
    {
        await seedstandarddata();
        var tool = new SearchDocumentsTool(createdocumentservice());
        var args = new JsonObject { ["query"] = "temperature", ["topK"] = 5 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"count\":0");
    }

    [Fact]
    public async Task invokeasync_with_matching_chunks_returns_results()
    {
        await seedstandarddata();
        var docService = createdocumentservice();
        var text = "When temperature exceeds 85 degrees Celsius, shut down the line immediately.";
        using var stream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(text));
        await docService.UploadAsync(stream, "temp_sop.txt", "text/plain", text.Length, "SOP", 1);

        var tool = new SearchDocumentsTool(docService);
        var args = new JsonObject { ["query"] = "temperature", ["topK"] = 5 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"count\":1");
    }

    [Fact]
    public async Task invokeasync_filters_by_category()
    {
        await seedstandarddata();
        var docService = createdocumentservice();

        var text1 = "temperature procedure for manufacturing";
        using var stream1 = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(text1));
        await docService.UploadAsync(stream1, "sop.txt", "text/plain", text1.Length, "SOP", 1);

        var text2 = "temperature specification for quality control";
        using var stream2 = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(text2));
        await docService.UploadAsync(stream2, "spec.txt", "text/plain", text2.Length, "Spec", 1);

        var tool = new SearchDocumentsTool(docService);
        var args = new JsonObject { ["query"] = "temperature", ["topK"] = 5, ["category"] = "SOP" };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        // Should only return SOP category results
        result.Output.Should().Contain("\"count\":1");
    }

    [Fact]
    public async Task invokeasync_with_empty_query_returns_failure()
    {
        await seedstandarddata();
        var tool = new SearchDocumentsTool(createdocumentservice());
        var args = new JsonObject { ["query"] = "", ["topK"] = 5 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeFalse();
    }

    [Fact]
    public async Task invokeasync_respects_topk_limit()
    {
        await seedstandarddata();
        var docService = createdocumentservice();

        // Upload a document with enough text to produce multiple chunks
        var paragraphs = new List<string>();
        for (var i = 0; i < 20; i++)
        {
            paragraphs.Add($"temperature chunk {i} with enough text to ensure multiple chunks are created for testing.");
        }
        var text = string.Join("\n\n", paragraphs);
        using var stream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(text));
        await docService.UploadAsync(stream, "multi.txt", "text/plain", text.Length, "SOP", 1);

        var tool = new SearchDocumentsTool(docService);
        var args = new JsonObject { ["query"] = "temperature", ["topK"] = 3 };
        var context = new ToolExecutionContext { UserId = 1, Roles = new List<string> { "Admin" } };

        var result = await tool.InvokeAsync(args, context);

        result.Success.Should().BeTrue();
        result.Output.Should().Contain("\"count\":3");
    }

    [Fact]
    public void tool_name_is_search_documents()
    {
        var tool = new SearchDocumentsTool(createdocumentservice());
        tool.Name.Should().Be("search_documents");
    }

    [Fact]
    public void tool_category_is_rag()
    {
        var tool = new SearchDocumentsTool(createdocumentservice());
        tool.Category.Should().Be("rag");
    }
}