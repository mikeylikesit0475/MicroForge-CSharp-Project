using MicroForge.Application.Dtos;
using MicroForge.Application.Services;
using MicroForge.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroForge.Web.Controllers;

/// <summary>
/// AI agent chat endpoint with tool-use capabilities.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Engineer")]
public class AgentController : ControllerBase
{
    private readonly IAgentAppService _agentAppService;
    private readonly IToolRegistry _toolRegistry;
    private readonly ILogger<AgentController> _logger;

    public AgentController(IAgentAppService agentAppService, IToolRegistry toolRegistry, ILogger<AgentController> logger)
    {
        _agentAppService = agentAppService;
        _toolRegistry = toolRegistry;
        _logger = logger;
    }

    /// <summary>
    /// Send a message to the AI agent and receive a response with tool invocation details.
    /// </summary>
    /// <param name="request">Chat request with user message.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Agent response with tools invoked and token usage.</returns>
    [HttpPost("chat")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Chat([FromBody] AgentRequestDto request, CancellationToken ct)
    {
        if (!ModelState.IsValid || string.IsNullOrWhiteSpace(request.Message))
            return BadRequest(new ProblemDetails
            {
                Title = "Invalid request",
                Detail = "Message is required",
                Status = StatusCodes.Status400BadRequest
            });

        _logger.LogInformation("Agent chat request from user {Username}", User.Identity?.Name);
        var result = await _agentAppService.ChatAsync(request, User, ct);
        return Ok(result);
    }

    /// <summary>
    /// List all available tools and their JSON schemas.
    /// </summary>
    /// <returns>List of tool definitions.</returns>
    [HttpGet("tools")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetTools()
    {
        var tools = _toolRegistry.GetAllTools().Select(t => new
        {
            name = t.Name,
            description = t.Description,
            category = t.Category,
            schema = t.Schema
        });
        return Ok(tools);
    }

    /// <summary>
    /// Get recent agent interaction history for the current user.
    /// </summary>
    /// <param name="limit">Maximum number of logs (default 20, max 100).</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>List of agent log entries.</returns>
    [HttpGet("history")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHistory([FromQuery] int limit = 20, CancellationToken ct = default)
    {
        if (limit > 100) limit = 100;
        var userId = GetUserId();
        var logs = await _agentAppService.GetRecentLogsAsync(userId, limit, ct);
        return Ok(logs);
    }

    private int GetUserId()
    {
        var idClaim = User.FindFirst("sub")?.Value ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0";
        return int.TryParse(idClaim, out var id) ? id : 0;
    }

    /// <summary>
    /// Fetch all available LLM models from the configured Ollama or local LLM server.
    /// </summary>
    [HttpGet("models")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetModels(CancellationToken ct)
    {
        var models = await _agentAppService.GetAvailableModelsAsync(ct);
        return Ok(models);
    }
}