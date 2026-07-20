using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MicroForge.Infrastructure.Data;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly MicroForgeDbContext _context;
    private readonly DbSet<T> _set;

    public Repository(MicroForgeDbContext context)
    {
        _context = context;
        _set = context.Set<T>();
    }

    public IQueryable<T> Query() => _set.AsQueryable();

    public async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _set.FindAsync(new object[] { id }, ct);
    }

    public async Task<List<T>> ListAsync(CancellationToken ct = default)
    {
        return await _set.ToListAsync(ct);
    }

    public async Task AddAsync(T entity, CancellationToken ct = default)
    {
        await _set.AddAsync(entity, ct);
    }

    public void Update(T entity)
    {
        _set.Update(entity);
    }

    public void Remove(T entity)
    {
        _set.Remove(entity);
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(ct);
    }
}