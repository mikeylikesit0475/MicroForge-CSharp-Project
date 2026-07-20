using MicroForge.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace MicroForge.Infrastructure.Cache;

public class CacheService : ICacheService
{
    private readonly IMemoryCache _cache;
    private static readonly ConcurrentDictionary<string, byte> _keys = new();

    public CacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Task<T?> GetAsync<T>(string key, CancellationToken ct = default)
    {
        _cache.TryGetValue(key, out T? value);
        return Task.FromResult(value);
    }

    public Task SetAsync<T>(string key, T value, TimeSpan ttl, CancellationToken ct = default)
    {
        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = ttl
        };
        options.RegisterPostEvictionCallback((key, value, reason, state) =>
        {
            if (key is string k) _keys.TryRemove(k, out _);
        });
        _cache.Set(key, value, options);
        _keys.TryAdd(key, 0);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key, CancellationToken ct = default)
    {
        _cache.Remove(key);
        _keys.TryRemove(key, out _);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(string key, CancellationToken ct = default)
    {
        return Task.FromResult(_keys.ContainsKey(key) && _cache.TryGetValue(key, out _));
    }
}