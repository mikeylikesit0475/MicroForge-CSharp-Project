namespace MicroForge.Domain.Interfaces;

/// <summary>
/// Authentication service for JWT generation and validation.
/// </summary>
public interface IAuthService
{
    Task<AuthResult> LoginAsync(string username, string password, CancellationToken ct = default);
    Task<AuthResult> RefreshAsync(string refreshToken, CancellationToken ct = default);
    Task<bool> RegisterAsync(string username, string email, string password, string firstName, string lastName, string[] roleNames, int[] productionLineIds, CancellationToken ct = default);
}

/// <summary>
/// Result of an authentication attempt.
/// </summary>
public class AuthResult
{
    public bool Success { get; init; }
    public string? AccessToken { get; init; }
    public string? RefreshToken { get; init; }
    public DateTime? ExpiresAt { get; init; }
    public string? ErrorMessage { get; init; }
    public string? Username { get; init; }
    public List<string> Roles { get; init; } = new();
}