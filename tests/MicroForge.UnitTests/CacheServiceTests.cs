using FluentAssertions;
using MicroForge.Infrastructure.Cache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace MicroForge.UnitTests;

/// <summary>
/// Unit tests for the CacheService — get/set/remove/exists with TTL.
/// </summary>
public class CacheServiceTests
{
    private readonly CacheService _cache;
    private readonly IMemoryCache _memorycache;

    public CacheServiceTests()
    {
        var services = new ServiceCollection();
        services.AddMemoryCache();
        var provider = services.BuildServiceProvider();
        _memorycache = provider.GetRequiredService<IMemoryCache>();
        _cache = new CacheService(_memorycache);
    }

    [Fact]
    public async Task getasync_for_nonexistent_key_returns_default()
    {
        var result = await _cache.GetAsync<string>("nonexistent");

        result.Should().BeNull();
    }

    [Fact]
    public async Task setasync_then_getasync_returns_value()
    {
        await _cache.SetAsync("key1", "value1", TimeSpan.FromMinutes(5));

        var result = await _cache.GetAsync<string>("key1");

        result.Should().Be("value1");
    }

    [Fact]
    public async Task existsasync_returns_true_for_existing_key()
    {
        await _cache.SetAsync("key2", 42, TimeSpan.FromMinutes(5));

        var exists = await _cache.ExistsAsync("key2");

        exists.Should().BeTrue();
    }

    [Fact]
    public async Task existsasync_returns_false_for_nonexistent_key()
    {
        var exists = await _cache.ExistsAsync("nope");

        exists.Should().BeFalse();
    }

    [Fact]
    public async Task removeasync_deletes_key()
    {
        await _cache.SetAsync("key3", "data", TimeSpan.FromMinutes(5));
        (await _cache.ExistsAsync("key3")).Should().BeTrue();

        await _cache.RemoveAsync("key3");

        (await _cache.ExistsAsync("key3")).Should().BeFalse();
        (await _cache.GetAsync<string>("key3")).Should().BeNull();
    }

    [Fact]
    public async Task setasync_with_complex_object_roundtrips_correctly()
    {
        var testobj = new TestPoco { Name = "widget", Count = 7 };

        await _cache.SetAsync("obj1", testobj, TimeSpan.FromMinutes(5));

        var result = await _cache.GetAsync<TestPoco>("obj1");
        result.Should().NotBeNull();
        result!.Name.Should().Be("widget");
        result.Count.Should().Be(7);
    }

    [Fact]
    public async Task setasync_overwrites_existing_value()
    {
        await _cache.SetAsync("key4", "first", TimeSpan.FromMinutes(5));
        await _cache.SetAsync("key4", "second", TimeSpan.FromMinutes(5));

        var result = await _cache.GetAsync<string>("key4");

        result.Should().Be("second");
    }

    private class TestPoco
    {
        public string Name { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}