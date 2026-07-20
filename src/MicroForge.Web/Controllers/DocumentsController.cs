using MicroForge.Application.Dtos;
using MicroForge.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroForge.Web.Controllers;

/// <summary>
/// Document upload, search, and RAG endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentAppService _documentAppService;
    private readonly ILogger<DocumentsController> _logger;

    public DocumentsController(IDocumentAppService documentAppService, ILogger<DocumentsController> logger)
    {
        _documentAppService = documentAppService;
        _logger = logger;
    }

    /// <summary>
    /// Upload a document for RAG ingestion. Requires Admin or Engineer role.
    /// </summary>
    /// <param name="file">The uploaded file.</param>
    /// <param name="category">Document category (e.g. SOP, Spec, Report).</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Upload result with document ID and chunk count.</returns>
    [HttpPost("upload")]
    [Authorize(Roles = "Admin,Engineer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] string category, CancellationToken ct)
    {
        if (file == null || file.Length == 0)
            return BadRequest(new ProblemDetails
            {
                Title = "Invalid file",
                Detail = "No file uploaded or file is empty",
                Status = StatusCodes.Status400BadRequest
            });

        if (string.IsNullOrWhiteSpace(category))
            return BadRequest(new ProblemDetails
            {
                Title = "Missing category",
                Detail = "Document category is required",
                Status = StatusCodes.Status400BadRequest
            });

        var userId = GetUserId();
        using var stream = file.OpenReadStream();
        var result = await _documentAppService.UploadAsync(
            stream, file.FileName, file.ContentType, file.Length, category, userId, ct);

        _logger.LogInformation("Document uploaded: {Title} ({ChunkCount} chunks)", result.Title, result.ChunkCount);
        return Ok(result);
    }

    /// <summary>
    /// List all documents, optionally filtered by category.
    /// </summary>
    /// <param name="category">Optional category filter.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>List of documents.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> List([FromQuery] string? category, CancellationToken ct)
    {
        var docs = await _documentAppService.ListAsync(category, ct);
        return Ok(docs);
    }

    /// <summary>
    /// Delete a document and all its chunks. Requires Admin role.
    /// </summary>
    /// <param name="id">Document ID.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>204 No Content.</returns>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        await _documentAppService.DeleteAsync(id, ct);
        return NoContent();
    }

    /// <summary>
    /// Semantic search across embedded documents.
    /// </summary>
    /// <param name="request">Search query with optional filters.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Ranked search results with similarity scores.</returns>
    [HttpPost("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Search([FromBody] DocumentSearchRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid || string.IsNullOrWhiteSpace(request.Query))
            return BadRequest(new ProblemDetails
            {
                Title = "Invalid search",
                Detail = "Query is required",
                Status = StatusCodes.Status400BadRequest
            });

        var results = await _documentAppService.SearchAsync(request.Query, request.TopK, request.Category, ct);
        return Ok(new { count = results.Count, results });
    }

    /// <summary>
    /// Ask a question against the document corpus — returns relevant excerpts as an answer.
    /// </summary>
    /// <param name="request">Question with optional category filter.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Answer with supporting document excerpts.</returns>
    [HttpPost("ask")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Ask([FromBody] DocumentAskRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid || string.IsNullOrWhiteSpace(request.Query))
            return BadRequest(new ProblemDetails
            {
                Title = "Invalid request",
                Detail = "Query is required",
                Status = StatusCodes.Status400BadRequest
            });

        var topK = request.TopK > 0 && request.TopK <= 20 ? request.TopK : 5;
        var results = await _documentAppService.SearchAsync(request.Query, topK, request.Category, ct);

        if (results.Count == 0)
            return Ok(new
            {
                answer = "No relevant documents found for your query.",
                sources = Array.Empty<object>()
            });

        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Based on the available documents, here are the most relevant findings:");
        sb.AppendLine();
        foreach (var r in results)
        {
            sb.AppendLine($"- {r.Content}");
            sb.AppendLine($"  (Similarity: {r.Similarity:F2}, Category: {r.Category})");
            sb.AppendLine();
        }

        return Ok(new
        {
            answer = sb.ToString(),
            sources = results
        });
    }

    private int GetUserId()
    {
        var idClaim = User.FindFirst("sub")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0";
        return int.TryParse(idClaim, out var id) ? id : 0;
    }
}

/// <summary>
/// Request model for document search.
/// </summary>
public class DocumentSearchRequest
{
    public string Query { get; set; } = string.Empty;
    public int TopK { get; set; } = 5;
    public string? Category { get; set; }
}

/// <summary>
/// Request model for asking questions against the document corpus.
/// </summary>
public class DocumentAskRequest
{
    public string Query { get; set; } = string.Empty;
    public int TopK { get; set; } = 5;
    public string? Category { get; set; }
}