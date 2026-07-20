using MicroForge.Application.Dtos;
using MicroForge.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MicroForge.Web.Controllers;

/// <summary>
/// Authentication endpoints — login, register, refresh, profile.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Authenticate a user and receive a JWT access token.
    /// </summary>
    /// <param name="request">Login credentials.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>JWT token with refresh token.</returns>
    /// <response code="200">Returns the access token and refresh token.</response>
    /// <response code="401">Invalid username or password.</response>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.LoginAsync(request.Username, request.Password, ct);
        if (!result.Success)
        {
            _logger.LogWarning("Failed login attempt for username: {Username}", request.Username);
            return Unauthorized(new ProblemDetails
            {
                Title = "Authentication failed",
                Detail = result.ErrorMessage ?? "Invalid credentials",
                Status = StatusCodes.Status401Unauthorized
            });
        }

        _logger.LogInformation("User {Username} logged in successfully", request.Username);

        return Ok(new LoginResponse
        {
            AccessToken = result.AccessToken!,
            RefreshToken = result.RefreshToken!,
            ExpiresAt = result.ExpiresAt!.Value,
            Username = result.Username!,
            Roles = result.Roles
        });
    }

    /// <summary>
    /// Register a new user account.
    /// </summary>
    /// <param name="request">Registration details.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>201 if created, 409 if username/email already exists.</returns>
    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = await _authService.RegisterAsync(
            request.Username, request.Email, request.Password,
            request.FirstName, request.LastName,
            request.Roles.ToArray(), request.ProductionLineIds.ToArray(), ct);

        if (!success)
            return Conflict(new ProblemDetails
            {
                Title = "Registration failed",
                Detail = "Username or email already exists",
                Status = StatusCodes.Status409Conflict
            });

        return StatusCode(StatusCodes.Status201Created, new { message = "User registered successfully" });
    }

    /// <summary>
    /// Refresh an expired access token using a refresh token.
    /// </summary>
    /// <param name="request">Refresh token.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>New access token or 400 if refresh fails.</returns>
    [HttpPost("refresh")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request, CancellationToken ct)
    {
        var result = await _authService.RefreshAsync(request.RefreshToken, ct);
        if (!result.Success)
            return BadRequest(new ProblemDetails
            {
                Title = "Token refresh failed",
                Detail = result.ErrorMessage,
                Status = StatusCodes.Status400BadRequest
            });

        return Ok(new LoginResponse
        {
            AccessToken = result.AccessToken!,
            RefreshToken = result.RefreshToken!,
            ExpiresAt = result.ExpiresAt!.Value,
            Username = result.Username!,
            Roles = result.Roles
        });
    }

    /// <summary>
    /// Get the current authenticated user's profile.
    /// </summary>
    /// <returns>User profile information.</returns>
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Me()
    {
        var userId = User.FindFirst("sub")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var username = User.Identity?.Name;
        var roles = User.Claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();
        var lineIds = User.FindFirst("productionLineIds")?.Value?
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse).ToList() ?? new List<int>();

        return Ok(new UserDto
        {
            Id = int.TryParse(userId, out var id) ? id : 0,
            Username = username ?? "",
            Email = User.FindFirst("email")?.Value ?? "",
            FirstName = "",
            LastName = "",
            IsActive = true,
            Roles = roles,
            ProductionLineIds = lineIds,
            CreatedAt = DateTime.UtcNow
        });
    }
}

/// <summary>
/// Request model for token refresh.
/// </summary>
public class RefreshTokenRequest
{
    public string RefreshToken { get; set; } = string.Empty;
}