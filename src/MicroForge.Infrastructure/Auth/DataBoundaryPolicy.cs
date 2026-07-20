using MicroForge.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MicroForge.Infrastructure.Auth;

public class DataBoundaryPolicy : IDataBoundaryPolicy
{
    public IQueryable<T> ApplyBoundary<T>(IQueryable<T> query, ClaimsPrincipal user) where T : class, IProductionLineBound
    {
        var allowedIds = GetAllowedLineIds(user);
        if (allowedIds.Count == 0)
            return query;
        return query.Where(x => allowedIds.Contains(x.ProductionLineId));
    }

    public List<int> GetAllowedLineIds(ClaimsPrincipal user)
    {
        if (user.IsInRole("Admin") || user.IsInRole("Engineer"))
            return new List<int>();

        var lineClaim = user.FindFirst("productionLineIds")?.Value;
        if (string.IsNullOrEmpty(lineClaim))
            return new List<int>();

        return lineClaim.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => int.TryParse(s.Trim(), out var id) ? id : 0)
            .Where(id => id > 0)
            .ToList();
    }

    public bool CanAccessLine(ClaimsPrincipal user, int lineId)
    {
        if (user.IsInRole("Admin") || user.IsInRole("Engineer"))
            return true;
        var allowedIds = GetAllowedLineIds(user);
        return allowedIds.Count == 0 || allowedIds.Contains(lineId);
    }
}