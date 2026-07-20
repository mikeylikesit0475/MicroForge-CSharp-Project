using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MicroForge.PerformanceTests;

/// <summary>
/// Lightweight load and performance baseline test for key MicroForge API endpoints.
/// Measures p50, p95, and p99 response times under concurrent load.
/// Run with: dotnet run --project tests/MicroForge.PerformanceTests -- [baseUrl]
/// Default base URL: http://localhost:5000
/// </summary>
public class PerformanceBaseline
{
    private static readonly HttpClient client = new() { Timeout = TimeSpan.FromSeconds(30) };
    private static readonly string baseUrl = args.Length > 0 ? args[0] : "http://localhost:5000";

    private static string[] args => Environment.GetCommandLineArgs().Skip(1).ToArray();

    public static async Task Main()
    {
        Console.WriteLine("=== MicroForge Performance Baseline ===");
        Console.WriteLine($"Target: {baseUrl}");
        Console.WriteLine($"Date: {DateTime.UtcNow:O}");
        Console.WriteLine();

        // Warm up
        Console.WriteLine("Warming up...");
        await client.GetAsync($"{baseUrl}/health");
        Console.WriteLine();

        var results = new List<EndpointResult>();

        // Test 1: Health endpoint (no auth)
        results.Add(await RunTest("GET /health", async () =>
        {
            return await client.GetAsync($"{baseUrl}/health");
        }, concurrentUsers: 10, totalRequests: 100));

        // Test 2: Ready endpoint (no auth)
        results.Add(await RunTest("GET /ready", async () =>
        {
            return await client.GetAsync($"{baseUrl}/ready");
        }, concurrentUsers: 10, totalRequests: 100));

        // Test 3: Metrics endpoint (no auth)
        results.Add(await RunTest("GET /metrics", async () =>
        {
            return await client.GetAsync($"{baseUrl}/metrics");
        }, concurrentUsers: 10, totalRequests: 100));

        // Authenticate for protected endpoints
        Console.WriteLine("Authenticating...");
        var token = await GetAuthToken();
        if (token != null)
        {
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // Test 4: Dashboard endpoint
            results.Add(await RunTest("GET /api/dashboard", async () =>
            {
                return await client.GetAsync($"{baseUrl}/api/dashboard");
            }, concurrentUsers: 10, totalRequests: 50));

            // Test 5: Production lines endpoint
            results.Add(await RunTest("GET /api/productionlines", async () =>
            {
                return await client.GetAsync($"{baseUrl}/api/productionlines");
            }, concurrentUsers: 10, totalRequests: 50));

            // Test 6: Agent tools endpoint
            results.Add(await RunTest("GET /api/agent/tools", async () =>
            {
                return await client.GetAsync($"{baseUrl}/api/agent/tools");
            }, concurrentUsers: 5, totalRequests: 20));

            // Test 7: Agent chat endpoint (slower — mock LLM)
            results.Add(await RunTest("POST /api/agent/chat", async () =>
            {
                var payload = new { message = "What is the production line status?" };
                return await client.PostAsJsonAsync($"{baseUrl}/api/agent/chat", payload);
            }, concurrentUsers: 3, totalRequests: 10));

            // Test 8: Documents search
            results.Add(await RunTest("POST /api/documents/search", async () =>
            {
                var payload = new { query = "safety procedures", topK = 5 };
                return await client.PostAsJsonAsync($"{baseUrl}/api/documents/search", payload);
            }, concurrentUsers: 5, totalRequests: 20));
        }
        else
        {
            Console.WriteLine("WARNING: Could not authenticate — skipping protected endpoint tests.");
            Console.WriteLine("Make sure the API is running and seeded with admin/admin123.");
        }

        // Print summary table
        Console.WriteLine();
        Console.WriteLine("=== Results Summary ===");
        Console.WriteLine();
        Console.WriteLine($"{"Endpoint",-30} {"Requests",-10} {"p50 (ms)",-12} {"p95 (ms)",-12} {"p99 (ms)",-12} {"Errors",-8} {"Pass"}");
        Console.WriteLine(new string('-', 100));

        foreach (var r in results)
        {
            var pass = r.ErrorCount == 0 && r.P95Ms <= GetThreshold(r.Endpoint);
            Console.WriteLine($"{r.Endpoint,-30} {r.TotalRequests,-10} {r.P50Ms,-12:F1} {r.P95Ms,-12:F1} {r.P99Ms,-12:F1} {r.ErrorCount,-8} {(pass ? "✅" : "❌")}");
        }

        Console.WriteLine();
        var allPass = results.All(r => r.ErrorCount == 0 && r.P95Ms <= GetThreshold(r.Endpoint));
        Console.WriteLine($"Overall: {(allPass ? "✅ ALL PASS" : "❌ SOME FAILED")}");

        // Write report file
        await WriteReport(results, allPass);
    }

    private static async Task<string?> GetAuthToken()
    {
        try
        {
            var payload = new { username = "admin", password = "admin123" };
            var response = await client.PostAsJsonAsync($"{baseUrl}/api/auth/login", payload);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<JsonDocument>();
                return content?.RootElement.GetProperty("accessToken").GetString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Auth failed: {ex.Message}");
        }
        return null;
    }

    private static async Task<EndpointResult> RunTest(string name, Func<Task<HttpResponseMessage>> action, int concurrentUsers, int totalRequests)
    {
        Console.WriteLine($"Testing {name} ({totalRequests} requests, {concurrentUsers} concurrent)...");

        var latencies = new List<double>();
        var errors = 0;
        var semaphore = new SemaphoreSlim(concurrentUsers);
        var tasks = new Task[totalRequests];

        for (var i = 0; i < totalRequests; i++)
        {
            tasks[i] = Task.Run(async () =>
            {
                await semaphore.WaitAsync();
                var sw = Stopwatch.StartNew();
                try
                {
                    var response = await action();
                    if (!response.IsSuccessStatusCode)
                    {
                        Interlocked.Increment(ref errors);
                    }
                }
                catch
                {
                    Interlocked.Increment(ref errors);
                }
                sw.Stop();
                lock (latencies)
                {
                    latencies.Add(sw.Elapsed.TotalMilliseconds);
                }
                semaphore.Release();
            });
        }

        await Task.WhenAll(tasks);

        latencies.Sort();
        return new EndpointResult
        {
            Endpoint = name,
            TotalRequests = totalRequests,
            ConcurrentUsers = concurrentUsers,
            P50Ms = Percentile(latencies, 50),
            P95Ms = Percentile(latencies, 95),
            P99Ms = Percentile(latencies, 99),
            ErrorCount = errors
        };
    }

    private static double Percentile(List<double> sorted, double percentile)
    {
        if (sorted.Count == 0) return 0;
        var index = (int)Math.Ceiling(percentile / 100.0 * sorted.Count) - 1;
        index = Math.Max(0, Math.Min(index, sorted.Count - 1));
        return sorted[index];
    }

    private static double GetThreshold(string endpoint)
    {
        if (endpoint.Contains("agent/chat")) return 5000;
        if (endpoint.Contains("documents/search")) return 2000;
        return 500;
    }

    private static async Task WriteReport(List<EndpointResult> results, bool allPass)
    {
        var sb = new StringBuilder();
        sb.AppendLine("# MicroForge — Performance Baseline Report");
        sb.AppendLine();
        sb.AppendLine($"> **Date:** {DateTime.UtcNow:O}");
        sb.AppendLine($"> **Target:** {baseUrl}");
        sb.AppendLine($"> **Overall:** {(allPass ? "✅ ALL PASS" : "❌ SOME FAILED")}");
        sb.AppendLine();
        sb.AppendLine("## Endpoint Results");
        sb.AppendLine();
        sb.AppendLine("| Endpoint | Requests | Concurrent | p50 (ms) | p95 (ms) | p99 (ms) | Errors | Threshold | Pass |");
        sb.AppendLine("|----------|----------|------------|----------|----------|----------|--------|-----------|------|");
        foreach (var r in results)
        {
            var threshold = GetThreshold(r.Endpoint);
            var pass = r.ErrorCount == 0 && r.P95Ms <= threshold;
            sb.AppendLine($"| {r.Endpoint} | {r.TotalRequests} | {r.ConcurrentUsers} | {r.P50Ms:F1} | {r.P95Ms:F1} | {r.P99Ms:F1} | {r.ErrorCount} | {threshold}ms | {(pass ? "✅" : "❌")} |");
        }
        sb.AppendLine();
        sb.AppendLine("## Acceptance Criteria (MF-038)");
        sb.AppendLine();
        sb.AppendLine("- [ ] Dashboard data endpoint p95 ≤ 500ms");
        sb.AppendLine("- [ ] Agent chat endpoint p95 ≤ 5000ms");
        sb.AppendLine("- [ ] All endpoints report p50, p95, p99");
        sb.AppendLine();

        var reportPath = "performance-baseline-report.md";
        await File.WriteAllTextAsync(reportPath, sb.ToString());
        Console.WriteLine($"Report written to: {reportPath}");
    }

    private class EndpointResult
    {
        public string Endpoint { get; set; } = "";
        public int TotalRequests { get; set; }
        public int ConcurrentUsers { get; set; }
        public double P50Ms { get; set; }
        public double P95Ms { get; set; }
        public double P99Ms { get; set; }
        public int ErrorCount { get; set; }
    }
}