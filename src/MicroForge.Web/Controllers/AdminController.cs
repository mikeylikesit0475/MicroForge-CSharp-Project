using MicroForge.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroForge.Infrastructure.Data;

namespace MicroForge.Web.Controllers;

/// <summary>
/// Admin endpoints for user management, role assignment, and system diagnostics.
/// </summary>
[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly MicroForgeDbContext _db;
    private readonly ILogger<AdminController> _logger;

    public AdminController(MicroForgeDbContext db, ILogger<AdminController> logger)
    {
        _db = db;
        _logger = logger;
    }

    /// <summary>
    /// List all users with their roles.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>List of users.</returns>
    [HttpGet("users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ListUsers(CancellationToken ct)
    {
        var users = await _db.Users
            .Select(u => new
            {
                u.Id,
                u.Username,
                u.Email,
                u.FirstName,
                u.LastName,
                u.IsActive,
                u.CreatedAt,
                Roles = _db.UserRoles
                    .Where(ur => ur.UserId == u.Id)
                    .Join(_db.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name)
                    .ToList()
            })
            .ToListAsync(ct);

        return Ok(users);
    }

    /// <summary>
    /// Update the roles assigned to a user.
    /// </summary>
    /// <param name="id">User ID.</param>
    /// <param name="roleNames">List of role names to assign.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>200 OK or 404 if user not found.</returns>
    [HttpPut("users/{id:int}/roles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUserRoles(int id, [FromBody] List<string> roleNames, CancellationToken ct)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id, ct);
        if (user == null)
            return NotFound(new ProblemDetails
            {
                Title = "Not found",
                Detail = $"User {id} not found",
                Status = StatusCodes.Status404NotFound
            });

        var existing = _db.UserRoles.Where(ur => ur.UserId == id);
        _db.UserRoles.RemoveRange(existing);

        foreach (var roleName in roleNames)
        {
            var role = await _db.Roles.FirstOrDefaultAsync(r => r.Name == roleName, ct);
            if (role != null)
                _db.UserRoles.Add(new Domain.Entities.UserRole { UserId = id, RoleId = role.Id });
        }

        await _db.SaveChangesAsync(ct);
        _logger.LogInformation("Updated roles for user {UserId}: {Roles}", id, string.Join(", ", roleNames));

        return Ok(new { message = "Roles updated successfully" });
    }

    /// <summary>
    /// Get detailed system health status including database and service checks.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Detailed health report.</returns>
    [HttpGet("system/health")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SystemHealth(CancellationToken ct)
    {
        var dbConnected = await _db.Database.CanConnectAsync(ct);
        var userCount = await _db.Users.CountAsync(ct);
        var lineCount = await _db.ProductionLines.CountAsync(ct);
        var alertCount = await _db.Alerts.CountAsync(a => !a.IsAcknowledged, ct);

        return Ok(new
        {
            status = dbConnected ? "Healthy" : "Degraded",
            database = new { connected = dbConnected },
            metrics = new
            {
                totalUsers = userCount,
                totalProductionLines = lineCount,
                openAlerts = alertCount
            },
            timestamp = DateTime.UtcNow
        });
    }

    /// <summary>
    /// Get runtime metrics for monitoring.
    /// </summary>
    /// <returns>Runtime metrics snapshot.</returns>
    [HttpGet("system/metrics")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult SystemMetrics()
    {
        var process = System.Diagnostics.Process.GetCurrentProcess();
        return Ok(new
        {
            memoryWorkingSetBytes = process.WorkingSet64,
            memoryPrivateMemorySizeBytes = process.PrivateMemorySize64,
            threads = process.Threads.Count,
            uptimeSeconds = (DateTime.UtcNow - process.StartTime.ToUniversalTime()).TotalSeconds,
            timestamp = DateTime.UtcNow
        });
    }
}