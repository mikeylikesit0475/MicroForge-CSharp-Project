using FluentAssertions;
using MicroForge.Domain.Configuration;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.AI;
using MicroForge.Infrastructure.Telemetry;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using System.Text.Json.Nodes;

namespace MicroForge.AgentTests;

/// <summary>
/// Tests for the AgentRuntime — the LLM ↔ Tool orchestration loop.
/// Uses a mock LLM client to simulate tool-call sequences deterministically.
/// </summary>
public class AgentRuntimeTests
{
    private readonly AgentSettings _settings = new()
    {
        MaxIterations = 5,
        PerToolTimeoutSeconds = 10,
        SessionTimeoutSeconds = 60,
        LlmProvider = "mock"
    };

    private static IToolRegistry CreateRegistry(params ITool[] tools)
    {
        return new ToolRegistry(tools, NullLogger<ToolRegistry>.Instance);
    }

    private static Mock<ITool> CreateMockTool(string name, string category, string output)
    {
        var mock = new Mock<ITool>();
        mock.SetupGet(t => t.Name).Returns(name);
        mock.SetupGet(t => t.Description).Returns($"Mock tool: {name}");
        mock.SetupGet(t => t.Category).Returns(category);
        mock.SetupGet(t => t.Schema).Returns(new JsonObject());
        mock.Setup(t => t.InvokeAsync(It.IsAny<JsonObject?>(), It.IsAny<ToolExecutionContext>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ToolResult.Ok(output));
        return mock;
    }

    [Fact]
    public async Task executeasync_with_no_tool_call_returns_llm_response_directly()
    {
        var llmClient = new MockLlmClient(new LlmResponse
        {
            Content = "Hello! I can help you with production data.",
            ToolCall = null,
            FinishReason = true,
            TokensUsed = 10
        });

        var runtime = new AgentRuntime(
            CreateRegistry(),
            llmClient,
            Options.Create(_settings),
            NullLogger<AgentRuntime>.Instance, new AppMetricsService());

        var request = new AgentRequest
        {
            Message = "Hello",
            UserId = 1,
            Username = "admin",
            Roles = new List<string> { "Admin" }
        };

        var result = await runtime.ExecuteAsync(request);

        result.IsSuccess.Should().BeTrue();
        result.Response.Should().Contain("Hello");
        result.ToolsInvoked.Should().BeEmpty();
        result.TokensUsed.Should().Be(10);
    }

    [Fact]
    public async Task executeasync_invokes_tool_then_returns_final_answer()
    {
        var mockTool = CreateMockTool("get_production_line_status", "production", "Line Alpha: Running, Line Beta: Idle");

        // First LLM response: request a tool call
        // Second LLM response: final answer based on tool result
        var llmClient = new MockLlmClient(
            new LlmResponse
            {
                Content = "Let me check the production line status.",
                ToolCall = new ToolCall { Name = "get_production_line_status", Arguments = new JsonObject() },
                FinishReason = false,
                TokensUsed = 5
            },
            new LlmResponse
            {
                Content = "Line Alpha is running and Line Beta is idle.",
                ToolCall = null,
                FinishReason = true,
                TokensUsed = 15
            }
        );

        var runtime = new AgentRuntime(
            CreateRegistry(mockTool.Object),
            llmClient,
            Options.Create(_settings),
            NullLogger<AgentRuntime>.Instance, new AppMetricsService());

        var request = new AgentRequest
        {
            Message = "What is the production line status?",
            UserId = 1,
            Username = "admin",
            Roles = new List<string> { "Admin" }
        };

        var result = await runtime.ExecuteAsync(request);

        result.IsSuccess.Should().BeTrue();
        result.Response.Should().Contain("Line Alpha is running");
        result.ToolsInvoked.Should().ContainSingle("get_production_line_status");
        result.TokensUsed.Should().Be(20);
    }

    [Fact]
    public async Task executeasync_handles_tool_not_found_gracefully()
    {
        var llmClient = new MockLlmClient(
            new LlmResponse
            {
                Content = "Let me call a non-existent tool.",
                ToolCall = new ToolCall { Name = "nonexistent_tool", Arguments = new JsonObject() },
                FinishReason = false,
                TokensUsed = 5
            },
            new LlmResponse
            {
                Content = "I couldn't find that tool, but I can still help.",
                ToolCall = null,
                FinishReason = true,
                TokensUsed = 10
            }
        );

        var runtime = new AgentRuntime(
            CreateRegistry(),
            llmClient,
            Options.Create(_settings),
            NullLogger<AgentRuntime>.Instance, new AppMetricsService());

        var result = await runtime.ExecuteAsync(new AgentRequest
        {
            Message = "test",
            UserId = 1,
            Username = "admin",
            Roles = new List<string> { "Admin" }
        });

        result.IsSuccess.Should().BeTrue();
        // Tool not found is not added to ToolsInvoked, but the conversation continues
        result.Response.Should().Contain("couldn't find that tool");
    }

    [Fact]
    public async Task executeasync_handles_tool_exception_and_continues()
    {
        var mockTool = new Mock<ITool>();
        mockTool.SetupGet(t => t.Name).Returns("failing_tool");
        mockTool.SetupGet(t => t.Description).Returns("A tool that throws");
        mockTool.SetupGet(t => t.Category).Returns("test");
        mockTool.SetupGet(t => t.Schema).Returns(new JsonObject());
        mockTool.Setup(t => t.InvokeAsync(It.IsAny<JsonObject?>(), It.IsAny<ToolExecutionContext>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Tool exploded"));

        var llmClient = new MockLlmClient(
            new LlmResponse
            {
                Content = "Calling failing tool.",
                ToolCall = new ToolCall { Name = "failing_tool", Arguments = new JsonObject() },
                FinishReason = false,
                TokensUsed = 5
            },
            new LlmResponse
            {
                Content = "The tool failed, but I can still respond.",
                ToolCall = null,
                FinishReason = true,
                TokensUsed = 10
            }
        );

        var runtime = new AgentRuntime(
            CreateRegistry(mockTool.Object),
            llmClient,
            Options.Create(_settings),
            NullLogger<AgentRuntime>.Instance, new AppMetricsService());

        var result = await runtime.ExecuteAsync(new AgentRequest
        {
            Message = "test",
            UserId = 1,
            Username = "admin",
            Roles = new List<string> { "Admin" }
        });

        result.IsSuccess.Should().BeTrue();
        result.ToolsInvoked.Should().Contain("failing_tool");
        result.Response.Should().Contain("tool failed");
    }

    [Fact]
    public async Task executeasync_stops_at_max_iterations()
    {
        // LLM always requests a tool call, never finishes
        var loopResponse = new LlmResponse
        {
            Content = "Calling tool again.",
            ToolCall = new ToolCall { Name = "loop_tool", Arguments = new JsonObject() },
            FinishReason = false,
            TokensUsed = 1
        };

        var mockTool = CreateMockTool("loop_tool", "test", "result");
        // Provide enough responses for all 3 iterations
        var llmClient = new MockLlmClient(loopResponse, loopResponse, loopResponse);

        var settings = new AgentSettings
        {
            MaxIterations = 3,
            PerToolTimeoutSeconds = 10,
            LlmProvider = "mock"
        };

        var runtime = new AgentRuntime(
            CreateRegistry(mockTool.Object),
            llmClient,
            Options.Create(settings),
            NullLogger<AgentRuntime>.Instance, new AppMetricsService());

        var result = await runtime.ExecuteAsync(new AgentRequest
        {
            Message = "loop",
            UserId = 1,
            Username = "admin",
            Roles = new List<string> { "Admin" }
        });

        result.IsSuccess.Should().BeTrue();
        result.Response.Should().Contain("maximum number of tool iterations");
        result.ToolsInvoked.Count.Should().Be(3);
    }

    [Fact]
    public async Task executeasync_passes_user_context_to_tool()
    {
        ToolExecutionContext? capturedContext = null;
        var mockTool = new Mock<ITool>();
        mockTool.SetupGet(t => t.Name).Returns("context_tool");
        mockTool.SetupGet(t => t.Description).Returns("Captures context");
        mockTool.SetupGet(t => t.Category).Returns("test");
        mockTool.SetupGet(t => t.Schema).Returns(new JsonObject());
        mockTool.Setup(t => t.InvokeAsync(It.IsAny<JsonObject?>(), It.IsAny<ToolExecutionContext>(), It.IsAny<CancellationToken>()))
            .Callback<JsonObject?, ToolExecutionContext, CancellationToken>((_, ctx, _) => capturedContext = ctx)
            .ReturnsAsync(ToolResult.Ok("ok"));

        var llmClient = new MockLlmClient(
            new LlmResponse
            {
                Content = "Calling context tool.",
                ToolCall = new ToolCall { Name = "context_tool", Arguments = new JsonObject() },
                FinishReason = false,
                TokensUsed = 5
            },
            new LlmResponse
            {
                Content = "Done.",
                ToolCall = null,
                FinishReason = true,
                TokensUsed = 5
            }
        );

        var runtime = new AgentRuntime(
            CreateRegistry(mockTool.Object),
            llmClient,
            Options.Create(_settings),
            NullLogger<AgentRuntime>.Instance, new AppMetricsService());

        var request = new AgentRequest
        {
            Message = "test",
            UserId = 42,
            Username = "engineer1",
            SessionId = "session-123",
            Roles = new List<string> { "Engineer" },
            ProductionLineIds = new List<int> { 1, 2 }
        };

        await runtime.ExecuteAsync(request);

        capturedContext.Should().NotBeNull();
        capturedContext!.UserId.Should().Be(42);
        capturedContext.Username.Should().Be("engineer1");
        capturedContext.SessionId.Should().Be("session-123");
        capturedContext.Roles.Should().Contain("Engineer");
        capturedContext.ProductionLineIds.Should().BeEquivalentTo(new[] { 1, 2 });
    }

    [Fact]
    public async Task executeasync_tracks_total_tokens_across_iterations()
    {
        var mockTool = CreateMockTool("token_tool", "test", "result");

        var llmClient = new MockLlmClient(
            new LlmResponse { Content = "call", ToolCall = new ToolCall { Name = "token_tool", Arguments = new JsonObject() }, FinishReason = false, TokensUsed = 7 },
            new LlmResponse { Content = "call", ToolCall = new ToolCall { Name = "token_tool", Arguments = new JsonObject() }, FinishReason = false, TokensUsed = 13 },
            new LlmResponse { Content = "final answer", ToolCall = null, FinishReason = true, TokensUsed = 20 }
        );

        var runtime = new AgentRuntime(
            CreateRegistry(mockTool.Object),
            llmClient,
            Options.Create(_settings),
            NullLogger<AgentRuntime>.Instance, new AppMetricsService());

        var result = await runtime.ExecuteAsync(new AgentRequest
        {
            Message = "test",
            UserId = 1,
            Username = "admin",
            Roles = new List<string> { "Admin" }
        });

        result.TokensUsed.Should().Be(40);
    }

    [Fact]
    public async Task executeasync_includes_system_prompt_with_tool_definitions()
    {
        var mockTool = CreateMockTool("test_tool", "production", "result");
        var llmClient = new MockLlmClient(new LlmResponse
        {
            Content = "Done.",
            ToolCall = null,
            FinishReason = true,
            TokensUsed = 5
        });

        var runtime = new AgentRuntime(
            CreateRegistry(mockTool.Object),
            llmClient,
            Options.Create(_settings),
            NullLogger<AgentRuntime>.Instance, new AppMetricsService());

        await runtime.ExecuteAsync(new AgentRequest
        {
            Message = "test",
            UserId = 1,
            Username = "admin",
            Roles = new List<string> { "Admin" }
        });

        llmClient.LastSystemPrompt.Should().NotBeNullOrEmpty();
        llmClient.LastSystemPrompt.Should().Contain("MicroForge AI Agent");
        llmClient.LastSystemPrompt.Should().Contain("test_tool");
    }

    [Fact]
    public async Task executeasync_returns_duration_ms()
    {
        var llmClient = new MockLlmClient(new LlmResponse
        {
            Content = "Done.",
            ToolCall = null,
            FinishReason = true,
            TokensUsed = 5
        });

        var runtime = new AgentRuntime(
            CreateRegistry(),
            llmClient,
            Options.Create(_settings),
            NullLogger<AgentRuntime>.Instance, new AppMetricsService());

        var result = await runtime.ExecuteAsync(new AgentRequest
        {
            Message = "test",
            UserId = 1,
            Username = "admin",
            Roles = new List<string> { "Admin" }
        });

        result.DurationMs.Should().BeGreaterThan(0);
    }

    /// <summary>
    /// Mock LLM client that returns pre-configured responses in sequence.
    /// </summary>
    private class MockLlmClient : ILlmClient
    {
        private readonly Queue<LlmResponse> _responses;
        public string? LastSystemPrompt { get; private set; }

        public MockLlmClient(params LlmResponse[] responses)
        {
            _responses = new Queue<LlmResponse>(responses);
        }

        public Task<LlmResponse> CompleteAsync(LlmRequest request, CancellationToken ct = default)
        {
            LastSystemPrompt = request.SystemPrompt;
            if (_responses.Count == 0)
                return Task.FromResult(new LlmResponse
                {
                    Content = "No more responses configured.",
                    ToolCall = null,
                    FinishReason = true,
                    TokensUsed = 0
                });
            return Task.FromResult(_responses.Dequeue());
        }
    }
}

/// <summary>
/// Tests for the ToolRegistry — tool registration, lookup, and system prompt generation.
/// </summary>
public class ToolRegistryAgentTests
{
    [Fact]
    public void register_adds_tool_to_registry()
    {
        var registry = new ToolRegistry(
            Enumerable.Empty<ITool>(),
            NullLogger<ToolRegistry>.Instance);

        var mock = new Mock<ITool>();
        mock.SetupGet(t => t.Name).Returns("test_tool");
        mock.SetupGet(t => t.Description).Returns("A test tool");
        mock.SetupGet(t => t.Category).Returns("test");
        mock.SetupGet(t => t.Schema).Returns(new JsonObject());

        registry.Register(mock.Object);

        registry.GetTool("test_tool").Should().NotBeNull();
        registry.GetAllTools().Should().HaveCount(1);
    }

    [Fact]
    public void gettoolsbycategory_returns_only_matching_tools()
    {
        var mock1 = new Mock<ITool>();
        mock1.SetupGet(t => t.Name).Returns("tool1");
        mock1.SetupGet(t => t.Category).Returns("production");
        mock1.SetupGet(t => t.Description).Returns("desc");
        mock1.SetupGet(t => t.Schema).Returns(new JsonObject());

        var mock2 = new Mock<ITool>();
        mock2.SetupGet(t => t.Name).Returns("tool2");
        mock2.SetupGet(t => t.Category).Returns("quality");
        mock2.SetupGet(t => t.Description).Returns("desc");
        mock2.SetupGet(t => t.Schema).Returns(new JsonObject());

        var registry = new ToolRegistry(new[] { mock1.Object, mock2.Object }, NullLogger<ToolRegistry>.Instance);

        var productionTools = registry.GetToolsByCategory("production");
        productionTools.Should().HaveCount(1);
        productionTools.First().Name.Should().Be("tool1");
    }

    [Fact]
    public void gettoolsassystemprompt_includes_all_tool_names()
    {
        var mock1 = new Mock<ITool>();
        mock1.SetupGet(t => t.Name).Returns("get_status");
        mock1.SetupGet(t => t.Description).Returns("Gets status");
        mock1.SetupGet(t => t.Category).Returns("production");
        mock1.SetupGet(t => t.Schema).Returns(new JsonObject());

        var mock2 = new Mock<ITool>();
        mock2.SetupGet(t => t.Name).Returns("query_defects");
        mock2.SetupGet(t => t.Description).Returns("Queries defects");
        mock2.SetupGet(t => t.Category).Returns("quality");
        mock2.SetupGet(t => t.Schema).Returns(new JsonObject());

        var registry = new ToolRegistry(new[] { mock1.Object, mock2.Object }, NullLogger<ToolRegistry>.Instance);

        var prompt = registry.GetToolsAsSystemPrompt();
        prompt.Should().Contain("get_status");
        prompt.Should().Contain("query_defects");
        prompt.Should().Contain("You have access to the following tools");
    }

    [Fact]
    public void gettool_returns_null_for_unknown_tool()
    {
        var registry = new ToolRegistry(Enumerable.Empty<ITool>(), NullLogger<ToolRegistry>.Instance);
        registry.GetTool("nonexistent").Should().BeNull();
    }
}

/// <summary>
/// Tests for the LlmService mock response generation — tool call routing heuristics.
/// </summary>
public class LlmServiceMockTests
{
    private static LlmService CreateService(string provider = "mock")
    {
        var settings = new AgentSettings
        {
            LlmProvider = provider,
            LlmModel = "mock-model",
            LlmEndpoint = ""
        };
        return new LlmService(new HttpClient(), Options.Create(settings), NullLogger<LlmService>.Instance);
    }

    [Fact]
    public async Task completeasync_with_mock_provider_returns_default_response()
    {
        var service = CreateService();
        var request = new LlmRequest
        {
            SystemPrompt = "You are a helpful assistant.",
            Messages = new List<AgentMessage>
            {
                new() { Role = "user", Content = "Hello, who are you?" }
            },
            AvailableTools = new List<ToolDefinition>()
        };

        var response = await service.CompleteAsync(request);

        response.Content.Should().NotBeNullOrEmpty();
        response.FinishReason.Should().BeTrue();
    }

    [Fact]
    public async Task completeasync_routes_defect_queries_to_defect_tool()
    {
        var service = CreateService();
        var request = new LlmRequest
        {
            SystemPrompt = "test",
            Messages = new List<AgentMessage>
            {
                new() { Role = "user", Content = "Show me recent defects" }
            },
            AvailableTools = new List<ToolDefinition>
            {
                new() { Name = "query_production_defects", Description = "Query defects" }
            }
        };

        var response = await service.CompleteAsync(request);

        response.ToolCall.Should().NotBeNull();
        response.ToolCall!.Name.Should().Be("query_production_defects");
    }

    [Fact]
    public async Task completeasync_routes_sensor_queries_to_sensor_tool()
    {
        var service = CreateService();
        var request = new LlmRequest
        {
            SystemPrompt = "test",
            Messages = new List<AgentMessage>
            {
                new() { Role = "user", Content = "What are the latest sensor readings?" }
            },
            AvailableTools = new List<ToolDefinition>
            {
                new() { Name = "query_sensor_readings", Description = "Query sensor readings" }
            }
        };

        var response = await service.CompleteAsync(request);

        response.ToolCall.Should().NotBeNull();
        response.ToolCall!.Name.Should().Be("query_sensor_readings");
    }

    [Fact]
    public async Task completeasync_routes_document_queries_to_search_tool()
    {
        var service = CreateService();
        var request = new LlmRequest
        {
            SystemPrompt = "test",
            Messages = new List<AgentMessage>
            {
                new() { Role = "user", Content = "Search the SOP documents for safety procedures" }
            },
            AvailableTools = new List<ToolDefinition>
            {
                new() { Name = "search_documents", Description = "Search documents" }
            }
        };

        var response = await service.CompleteAsync(request);

        response.ToolCall.Should().NotBeNull();
        response.ToolCall!.Name.Should().Be("search_documents");
    }

    [Fact]
    public async Task completeasync_summarizes_tool_results_when_present()
    {
        var service = CreateService();
        var request = new LlmRequest
        {
            SystemPrompt = "test",
            Messages = new List<AgentMessage>
            {
                new() { Role = "user", Content = "What is the status?" },
                new() { Role = "assistant", Content = "Let me check.", ToolName = "get_production_line_status" },
                new() { Role = "tool", Content = "Line Alpha: Running", ToolName = "get_production_line_status" }
            },
            AvailableTools = new List<ToolDefinition>()
        };

        var response = await service.CompleteAsync(request);

        response.Content.Should().Contain("Line Alpha: Running");
        response.FinishReason.Should().BeTrue();
    }

    [Fact]
    public async Task completeasync_does_not_route_when_tool_not_available()
    {
        var service = CreateService();
        var request = new LlmRequest
        {
            SystemPrompt = "test",
            Messages = new List<AgentMessage>
            {
                new() { Role = "user", Content = "Show me recent defects" }
            },
            AvailableTools = new List<ToolDefinition>
            {
                new() { Name = "some_other_tool", Description = "Not a defect tool" }
            }
        };

        var response = await service.CompleteAsync(request);

        response.ToolCall.Should().BeNull();
    }
}