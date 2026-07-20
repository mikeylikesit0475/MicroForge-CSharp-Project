using System.Net.Http.Headers;
using System.Net.Http.Json;
using MicroForge.Application.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace MicroForge.Web.Services;

/// <summary>
/// Manages authentication state for Blazor Server — stores the JWT token,
/// provides an HttpClient pre-configured with the bearer token, and
/// integrates with Blazor's AuthenticationStateProvider for AuthorizeView.
/// </summary>
public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _http;
    private readonly NavigationManager _nav;
    private readonly ILogger<AuthStateProvider> _logger;

    public string? AccessToken { get; private set; }
    public string? RefreshTokenValue { get; private set; }
    public string Username { get; private set; } = string.Empty;
    public List<string> Roles { get; private set; } = new();
    public bool IsAuthenticated => !string.IsNullOrEmpty(AccessToken);
    public bool IsAdmin => Roles.Contains("Admin");
    public bool IsEngineer => Roles.Contains("Engineer");
    public bool IsOperator => Roles.Contains("Operator");

    public event Action? OnChange;

    public AuthStateProvider(HttpClient http, NavigationManager nav, ILogger<AuthStateProvider> logger)
    {
        _http = http;
        _nav = nav;
        _logger = logger;

        // Always use the actual server URL from NavigationManager.
        // The base address configured in Program.cs is a hardcoded default
        // (e.g. http://localhost:5188) that may not match the real port
        // the server is listening on.
        _http.BaseAddress = new Uri(_nav.BaseUri);
    }

    /// <summary>
    /// Attempt to log in with the given credentials. On success, stores tokens and notifies subscribers.
    /// </summary>
    public async Task<bool> LoginAsync(string username, string password)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("/api/auth/login", new LoginRequest
            {
                Username = username,
                Password = password
            });

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (result == null)
                return false;

            AccessToken = result.AccessToken;
            RefreshTokenValue = result.RefreshToken;
            Username = result.Username;
            Roles = result.Roles ?? new();
            SetAuthHeader();
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            OnChange?.Invoke();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login failed for {Username}", username);
            return false;
        }
    }

    /// <summary>
    /// Clear all auth state and redirect to login.
    /// </summary>
    public void Clear()
    {
        AccessToken = null;
        RefreshTokenValue = null;
        Username = string.Empty;
        Roles = new();
        _http.DefaultRequestHeaders.Authorization = null;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        OnChange?.Invoke();
    }

    /// <summary>
    /// Get the HttpClient with the current bearer token attached.
    /// </summary>
    public HttpClient GetClient()
    {
        if (!string.IsNullOrEmpty(AccessToken))
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
        return _http;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (!IsAuthenticated)
            return Task.FromResult(new AuthenticationState(new System.Security.Claims.ClaimsPrincipal(new System.Security.Claims.ClaimsIdentity())));

        var claims = new List<System.Security.Claims.Claim>
        {
            new(System.Security.Claims.ClaimTypes.Name, Username),
        };
        foreach (var role in Roles)
            claims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, role));

        var identity = new System.Security.Claims.ClaimsIdentity(claims, "MicroForgeAuth");
        return Task.FromResult(new AuthenticationState(new System.Security.Claims.ClaimsPrincipal(identity)));
    }

    private void SetAuthHeader()
    {
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
    }
}