namespace MicroForge.Domain.Entities;

/// <summary>
/// Audit trail of all AI agent interactions.
/// </summary>
public class AgentLog
{
    public long Id { get; set; }
    public int UserId { get; set; }
    public string SessionId { get; set; } = string.Empty;
    public string UserMessage { get; set; } = string.Empty;
    public string AgentResponse { get; set; } = string.Empty;
    public string ToolsInvoked { get; set; } = string.Empty;
    public int TokensUsed { get; set; }
    public decimal DurationMs { get; set; }
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}