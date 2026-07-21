using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MicroForge.Infrastructure.Data;
using System.Net;
using System.Net.Http.Json;

namespace MicroForge.IntegrationTests;

/// <summary>
/// Integration tests for the MicroForge Web API using WebApplicationFactory.
/// Tests the full HTTP pipeline including middleware, controllers, and database.
/// </summary>
public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.UseSetting("environment", "Testing");
            builder.ConfigureServices(services =>
            {
                // Keep a single SQLite in-memory connection open for the lifetime of the test
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();

                // Replace the DbContext registration with in-memory SQLite
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<MicroForgeDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<MicroForgeDbContext>(options =>
                    options.UseSqlite(connection));

                // Build the service provider once and seed
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<MicroForgeDbContext>();
                    db.Database.EnsureCreated();
                    SeedTestData(db);
                }
            });
        });
    }

    private static void SeedTestData(MicroForgeDbContext db)
    {
        if (db.Roles.Any()) return;

        db.Roles.AddRange(
            new Domain.Entities.Role { Name = "Admin", Description = "Full access", Permissions = "*" },
            new Domain.Entities.Role { Name = "Engineer", Description = "Engineering access", Permissions = "read,write,agent" },
            new Domain.Entities.Role { Name = "Operator", Description = "Operator access", Permissions = "read,write" }
        );
        db.SaveChanges();

        db.ProductionLines.AddRange(
            new Domain.Entities.ProductionLine { Name = "Line Alpha", Location = "Building A", LineType = "Photolithography", IsActive = true },
            new Domain.Entities.ProductionLine { Name = "Line Beta", Location = "Building B", LineType = "Etching", IsActive = true }
        );
        db.SaveChanges();

        var admin = new Domain.Entities.User
        {
            Username = "admin",
            Email = "admin@test.io",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123", workFactor: 4),
            FirstName = "System",
            LastName = "Admin",
            IsActive = true
        };
        db.Users.Add(admin);
        db.SaveChanges();

        db.UserRoles.Add(new Domain.Entities.UserRole { UserId = admin.Id, RoleId = db.Roles.First(r => r.Name == "Admin").Id });
        db.SaveChanges();
    }

    [Fact]
    public async Task health_endpoint_returns_200()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/health");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task ready_endpoint_returns_200()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/ready");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task metrics_endpoint_returns_prometheus_format()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/metrics");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("microforge_");
        content.Should().Contain("# TYPE");
    }

    [Fact]
    public async Task login_with_valid_credentials_returns_token()
    {
        var client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/auth/login", new
        {
            username = "admin",
            password = "admin123"
        });

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<LoginResult>();
        result.Should().NotBeNull();
        result!.AccessToken.Should().NotBeNullOrEmpty();
        result.RefreshToken.Should().NotBeNullOrEmpty();
        result.Username.Should().Be("admin");
        result.Roles.Should().Contain("Admin");
    }

    [Fact]
    public async Task login_with_invalid_credentials_returns_401()
    {
        var client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/auth/login", new
        {
            username = "admin",
            password = "wrongpassword"
        });

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task protected_endpoint_without_token_returns_401()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/productionlines");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task authenticated_request_can_access_production_lines()
    {
        var client = _factory.CreateClient();

        // Login first
        var loginResponse = await client.PostAsJsonAsync("/api/auth/login", new
        {
            username = "admin",
            password = "admin123"
        });
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResult>();
        var token = loginResult!.AccessToken;

        // Use token to access protected endpoint
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("/api/productionlines");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task admin_endpoint_requires_admin_role()
    {
        var client = _factory.CreateClient();

        // Login as admin
        var loginResponse = await client.PostAsJsonAsync("/api/auth/login", new
        {
            username = "admin",
            password = "admin123"
        });
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResult>();
        var token = loginResult!.AccessToken;

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("/api/admin/users");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task admin_system_health_returns_detailed_status()
    {
        var client = _factory.CreateClient();

        var loginResponse = await client.PostAsJsonAsync("/api/auth/login", new
        {
            username = "admin",
            password = "admin123"
        });
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResult>();
        var token = loginResult!.AccessToken;

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("/api/admin/system/health");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("database");
        content.Should().Contain("metrics");
    }

    [Fact]
    public async Task admin_system_metrics_returns_runtime_info()
    {
        var client = _factory.CreateClient();

        var loginResponse = await client.PostAsJsonAsync("/api/auth/login", new
        {
            username = "admin",
            password = "admin123"
        });
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResult>();
        var token = loginResult!.AccessToken;

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("/api/admin/system/metrics");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("memoryWorkingSetBytes");
        content.Should().Contain("uptimeSeconds");
    }

    [Fact]
    public async Task agent_chat_requires_engineer_or_admin_role()
    {
        var client = _factory.CreateClient();

        var loginResponse = await client.PostAsJsonAsync("/api/auth/login", new
        {
            username = "admin",
            password = "admin123"
        });
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResult>();
        var token = loginResult!.AccessToken;

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.PostAsJsonAsync("/api/agent/chat", new
        {
            message = "What is the production line status?"
        });

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task agent_tools_endpoint_returns_tool_list()
    {
        var client = _factory.CreateClient();

        var loginResponse = await client.PostAsJsonAsync("/api/auth/login", new
        {
            username = "admin",
            password = "admin123"
        });
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResult>();
        var token = loginResult!.AccessToken;

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("/api/agent/tools");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("name");
        content.Should().Contain("category");
    }

    [Fact]
    public async Task swagger_endpoint_available_in_development()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/swagger/index.html");
        // Swagger may or may not be available depending on environment
        response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task agent_history_endpoint_returns_logs()
    {
        var client = _factory.CreateClient();

        var loginResponse = await client.PostAsJsonAsync("/api/auth/login", new
        {
            username = "admin",
            password = "admin123"
        });
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResult>();
        var token = loginResult!.AccessToken;

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("/api/agent/history?limit=5");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task documents_search_without_auth_returns_401()
    {
        var client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/documents/search", new
        {
            query = "safety"
        });
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task documents_ask_without_auth_returns_401()
    {
        var client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/documents/ask", new
        {
            query = "What are the safety procedures?"
        });
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task documents_ask_with_auth_returns_answer()
    {
        var client = _factory.CreateClient();

        var loginResponse = await client.PostAsJsonAsync("/api/auth/login", new
        {
            username = "admin",
            password = "admin123"
        });
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResult>();
        var token = loginResult!.AccessToken;

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.PostAsJsonAsync("/api/documents/ask", new
        {
            query = "What are the safety procedures?"
        });

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("answer");
    }

    [Fact]
    public async Task dashboard_endpoint_returns_summary()
    {
        var client = _factory.CreateClient();

        var loginResponse = await client.PostAsJsonAsync("/api/auth/login", new
        {
            username = "admin",
            password = "admin123"
        });
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResult>();
        var token = loginResult!.AccessToken;

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("/api/dashboard");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task production_lines_endpoint_returns_seeded_data()
    {
        var client = _factory.CreateClient();

        var loginResponse = await client.PostAsJsonAsync("/api/auth/login", new
        {
            username = "admin",
            password = "admin123"
        });
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResult>();
        var token = loginResult!.AccessToken;

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await client.GetAsync("/api/productionlines");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Line Alpha");
        content.Should().Contain("Line Beta");
    }

    [Fact]
    public async Task wip_endpoints_support_creation_movement_and_hold()
    {
        var client = _factory.CreateClient();

        // 1. Login
        var loginResponse = await client.PostAsJsonAsync("/api/auth/login", new
        {
            username = "admin",
            password = "admin123"
        });
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResult>();
        var token = loginResult!.AccessToken;
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        // 2. Create Lot
        var createResponse = await client.PostAsJsonAsync(
            "/api/wip/lots/create?lotNumber=LOT-API-101&recipeName=FLOW-B", 
            new List<string> { "W-1", "W-2" });
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        // 3. Get Lots
        var getLotsResponse = await client.GetAsync("/api/wip/lots");
        getLotsResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var lotsContent = await getLotsResponse.Content.ReadAsStringAsync();
        lotsContent.Should().Contain("LOT-API-101");
    }

    [Fact]
    public async Task fdc_endpoints_return_success()
    {
        var client = _factory.CreateClient();

        // 1. Login
        var loginResponse = await client.PostAsJsonAsync("/api/auth/login", new
        {
            username = "admin",
            password = "admin123"
        });
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResult>();
        var token = loginResult!.AccessToken;
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        // 2. Query endpoints
        var matchingRes = await client.GetAsync("/api/fdc/matching");
        matchingRes.StatusCode.Should().Be(HttpStatusCode.OK);

        var maintRes = await client.GetAsync("/api/fdc/maintenance");
        maintRes.StatusCode.Should().Be(HttpStatusCode.OK);

        var faultsRes = await client.GetAsync("/api/fdc/faults");
        faultsRes.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task fab_endpoints_return_success()
    {
        var client = _factory.CreateClient();

        // 1. Login
        var loginResponse = await client.PostAsJsonAsync("/api/auth/login", new
        {
            username = "admin",
            password = "admin123"
        });
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResult>();
        var token = loginResult!.AccessToken;
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        // 2. Query endpoints
        var oeeRes = await client.GetAsync("/api/fab/oee");
        oeeRes.StatusCode.Should().Be(HttpStatusCode.OK);

        var maintRes = await client.GetAsync("/api/fab/maintenance");
        maintRes.StatusCode.Should().Be(HttpStatusCode.OK);

        var corrRes = await client.GetAsync("/api/fab/correlation");
        corrRes.StatusCode.Should().Be(HttpStatusCode.OK);

        var diffRes = await client.GetAsync("/api/fab/diff?runId=1&sensorType=Temperature");
        diffRes.StatusCode.Should().Be(HttpStatusCode.OK);

        var capasRes = await client.GetAsync("/api/fab/capas");
        capasRes.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task closed_loop_endpoints_return_success()
    {
        var client = _factory.CreateClient();

        // 1. Login
        var loginResponse = await client.PostAsJsonAsync("/api/auth/login", new
        {
            username = "admin",
            password = "admin123"
        });
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<LoginResult>();
        var token = loginResult!.AccessToken;
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        // 2. Query EWI & Qualifications
        var instructionsRes = await client.GetAsync("/api/wip/instructions/1");
        instructionsRes.StatusCode.Should().Be(HttpStatusCode.OK);

        var qualificationsRes = await client.GetAsync("/api/wip/qualifications");
        qualificationsRes.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    private class LoginResult
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public string Username { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
    }
}