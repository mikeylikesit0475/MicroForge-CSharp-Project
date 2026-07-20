using System.Security.Claims;

namespace MicroForge.Domain.Interfaces;

/// <summary>
/// Marker interface for entities bound to a production line (for data boundary enforcement).
/// </summary>
public interface IProductionLineBound
{
    int ProductionLineId { get; set; }
}

/// <summary>
/// Enforces data boundary policies — users only see data for their assigned production lines.
/// </summary>
public interface IDataBoundaryPolicy
{
    IQueryable<T> ApplyBoundary<T>(IQueryable<T> query, ClaimsPrincipal user) where T : class, IProductionLineBound;
    List<int> GetAllowedLineIds(ClaimsPrincipal user);
    bool CanAccessLine(ClaimsPrincipal user, int lineId);
}