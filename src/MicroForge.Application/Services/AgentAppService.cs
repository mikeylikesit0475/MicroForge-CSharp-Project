using MicroForge.Application.Dtos;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MicroForge.Application.Services;

public class AgentAppService : IAgentAppService
{
    private readonly IAgentRuntime _agentRuntime;
    private readonly IRepository<AgentLog> _logRepo;

    public AgentAppService(IAgentRuntime agentRuntime, IRepository<AgentLog> logRepo)
    {
        _agentRuntime = agentRuntime;
        _logRepo = logRepo;
    }

    public async Task<AgentResponseDto> ChatAsync(AgentRequestDto request, ClaimsPrincipal user, CancellationToken ct = default)
    {
        var userId = GetUserId(user);
        var username = user.Identity?.Name ?? "unknown";
        var roles = user.Claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();
        var lineIds = user.Claims.Where(c => c.Type == "productionLineIds")
            .SelectMany(c => c.Value.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .Select(int.Parse).ToList();
        var sessionId = string.IsNullOrEmpty(request.SessionId) ? Guid.NewGuid().ToString() : request.SessionId;

        var agentRequest = new AgentRequest
        {
            Message = request.Message,
            SessionId = sessionId,
            UserId = userId,
            Username = username,
            Roles = roles,
            ProductionLineIds = lineIds
        };

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var result = await _agentRuntime.ExecuteAsync(agentRequest, ct);
        sw.Stop();

        var log = new AgentLog
        {
            UserId = userId,
            SessionId = sessionId,
            UserMessage = request.Message,
            AgentResponse = result.Response,
            ToolsInvoked = string.Join(", ", result.ToolsInvoked),
            TokensUsed = result.TokensUsed,
            DurationMs = (decimal)sw.Elapsed.TotalMilliseconds,
            IsSuccess = result.IsSuccess,
            ErrorMessage = result.ErrorMessage,
            Timestamp = DateTime.UtcNow
        };
        await _logRepo.AddAsync(log, ct);
        await _logRepo.SaveChangesAsync(ct);

        return new AgentResponseDto
        {
            Response = result.Response,
            ToolsInvoked = result.ToolsInvoked,
            TokensUsed = result.TokensUsed,
            DurationMs = (decimal)sw.Elapsed.TotalMilliseconds,
            IsSuccess = result.IsSuccess,
            ErrorMessage = result.ErrorMessage
        };
    }

    public async Task<List<AgentLogSummary>> GetRecentLogsAsync(int userId, int limit, CancellationToken ct = default)
    {
        var logs = await _logRepo.Query()
            .Where(l => l.UserId == userId)
            .OrderByDescending(l => l.Timestamp)
            .Take(limit)
            .Select(l => new AgentLogSummary
            {
                Id = l.Id,
                UserMessage = l.UserMessage,
                AgentResponse = l.AgentResponse,
                ToolsInvoked = l.ToolsInvoked,
                Timestamp = l.Timestamp,
                IsSuccess = l.IsSuccess
            })
            .ToListAsync(ct);
        return logs;
    }

    private static int GetUserId(ClaimsPrincipal user)
    {
        var idClaim = user.FindFirst("sub")?.Value ?? user.FindFirst("nameidentifier")?.Value ?? "0";
        return int.TryParse(idClaim, out var id) ? id : 0;
    }
}