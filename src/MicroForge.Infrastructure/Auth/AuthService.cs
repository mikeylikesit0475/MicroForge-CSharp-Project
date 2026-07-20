using MicroForge.Domain.Configuration;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace MicroForge.Infrastructure.Auth;

public class AuthService : IAuthService
{
    private readonly IRepository<User> _userRepo;
    private readonly IRepository<Role> _roleRepo;
    private readonly IRepository<UserRole> _userRoleRepo;
    private readonly IRepository<UserProductionLine> _userLineRepo;
    private readonly JwtSettings _jwtSettings;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IRepository<User> userRepo,
        IRepository<Role> roleRepo,
        IRepository<UserRole> userRoleRepo,
        IRepository<UserProductionLine> userLineRepo,
        JwtSettings jwtSettings,
        ILogger<AuthService> logger)
    {
        _userRepo = userRepo;
        _roleRepo = roleRepo;
        _userRoleRepo = userRoleRepo;
        _userLineRepo = userLineRepo;
        _jwtSettings = jwtSettings;
        _logger = logger;
    }

    public async Task<AuthResult> LoginAsync(string username, string password, CancellationToken ct = default)
    {
        var user = await _userRepo.Query()
            .FirstOrDefaultAsync(u => u.Username == username, ct);
        if (user == null || !user.IsActive)
            return new AuthResult { Success = false, ErrorMessage = "Invalid username or password" };

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return new AuthResult { Success = false, ErrorMessage = "Invalid username or password" };

        var roles = await GetUserRolesAsync(user.Id, ct);
        var lineIds = await GetUserLineIdsAsync(user.Id, ct);

        var token = GenerateToken(user, roles, lineIds);
        var refreshToken = GenerateRefreshToken();

        _logger.LogInformation("User {Username} logged in successfully", username);

        return new AuthResult
        {
            Success = true,
            AccessToken = token,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiryMinutes),
            Username = user.Username,
            Roles = roles
        };
    }

    public Task<AuthResult> RefreshAsync(string refreshToken, CancellationToken ct = default)
    {
        // Simplified: in production, store and validate refresh tokens
        return Task.FromResult(new AuthResult
        {
            Success = false,
            ErrorMessage = "Refresh token rotation not implemented in this build"
        });
    }

    public async Task<bool> RegisterAsync(string username, string email, string password, string firstName, string lastName, string[] roleNames, int[] productionLineIds, CancellationToken ct = default)
    {
        if (await _userRepo.Query().AnyAsync(u => u.Username == username, ct))
            return false;
        if (await _userRepo.Query().AnyAsync(u => u.Email == email, ct))
            return false;

        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12),
            FirstName = firstName,
            LastName = lastName,
            IsActive = true
        };
        await _userRepo.AddAsync(user, ct);
        await _userRepo.SaveChangesAsync(ct);

        foreach (var roleName in roleNames)
        {
            var role = await _roleRepo.Query().FirstOrDefaultAsync(r => r.Name == roleName, ct);
            if (role != null)
            {
                await _userRoleRepo.AddAsync(new UserRole { UserId = user.Id, RoleId = role.Id }, ct);
            }
        }

        foreach (var lineId in productionLineIds)
        {
            await _userLineRepo.AddAsync(new UserProductionLine { UserId = user.Id, ProductionLineId = lineId }, ct);
        }

        await _userRepo.SaveChangesAsync(ct);
        return true;
    }

    private string GenerateToken(User user, List<string> roles, List<int> lineIds)
    {
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.Username),
            new("username", user.Username),
            new("email", user.Email)
        };
        foreach (var role in roles)
            claims.Add(new Claim("role", role));
        if (lineIds.Count > 0)
            claims.Add(new Claim("productionLineIds", string.Join(",", lineIds)));

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string GenerateRefreshToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(32);
        return Convert.ToBase64String(bytes);
    }

    private async Task<List<string>> GetUserRolesAsync(int userId, CancellationToken ct)
    {
        return await _userRoleRepo.Query()
            .Where(ur => ur.UserId == userId)
            .Join(_roleRepo.Query(), ur => ur.RoleId, r => r.Id, (ur, r) => r.Name)
            .ToListAsync(ct);
    }

    private async Task<List<int>> GetUserLineIdsAsync(int userId, CancellationToken ct)
    {
        return await _userLineRepo.Query()
            .Where(up => up.UserId == userId)
            .Select(up => up.ProductionLineId)
            .ToListAsync(ct);
    }
}