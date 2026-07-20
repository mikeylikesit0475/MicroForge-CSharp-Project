using MicroForge.Domain.Interfaces;
using MicroForge.Domain.Configuration;
using Microsoft.Extensions.Options;
using System.Text;

namespace MicroForge.Infrastructure.Rag;

public class DocumentChunker : IDocumentChunker
{
    private readonly int _chunkSizeTokens;
    private readonly int _overlapTokens;

    public DocumentChunker(IOptions<RagSettings> settings)
    {
        _chunkSizeTokens = settings.Value.ChunkSizeTokens;
        _overlapTokens = settings.Value.ChunkOverlapTokens;
    }

    public List<DocumentChunkResult> Chunk(string text, int? chunkSizeTokens = null, int? overlapTokens = null)
    {
        var chunkSize = chunkSizeTokens ?? _chunkSizeTokens;
        var overlap = overlapTokens ?? _overlapTokens;
        var results = new List<DocumentChunkResult>();

        if (string.IsNullOrWhiteSpace(text))
            return results;

        var paragraphs = text.Split(new[] { "\n\n", "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        var currentChunk = new StringBuilder();
        var currentTokens = 0;
        var chunkIndex = 0;
        var pageNumber = 1;

        foreach (var paragraph in paragraphs)
        {
            var paraTokens = EstimateTokens(paragraph);

            if (currentTokens + paraTokens > chunkSize && currentChunk.Length > 0)
            {
                results.Add(new DocumentChunkResult
                {
                    ChunkIndex = chunkIndex++,
                    Content = currentChunk.ToString().Trim(),
                    TokenCount = currentTokens,
                    PageNumber = pageNumber,
                    Metadata = $"{{\"page\":{pageNumber}}}"
                });

                if (overlap > 0 && currentTokens > overlap)
                {
                    var overlapText = GetLastTokens(currentChunk.ToString(), overlap);
                    currentChunk.Clear();
                    currentChunk.Append(overlapText);
                    currentTokens = EstimateTokens(overlapText);
                }
                else
                {
                    currentChunk.Clear();
                    currentTokens = 0;
                }
            }

            currentChunk.AppendLine(paragraph.Trim());
            currentTokens += paraTokens;

            if (paragraph.Contains("\f"))
                pageNumber++;
        }

        if (currentChunk.Length > 0)
        {
            results.Add(new DocumentChunkResult
            {
                ChunkIndex = chunkIndex,
                Content = currentChunk.ToString().Trim(),
                TokenCount = currentTokens,
                PageNumber = pageNumber,
                Metadata = $"{{\"page\":{pageNumber}}}"
            });
        }

        return results;
    }

    private static int EstimateTokens(string text)
    {
        return (int)Math.Ceiling(text.Length / 4.0);
    }

    private static string GetLastTokens(string text, int tokenCount)
    {
        var charCount = tokenCount * 4;
        if (text.Length <= charCount) return text;
        return text[^charCount..];
    }
}