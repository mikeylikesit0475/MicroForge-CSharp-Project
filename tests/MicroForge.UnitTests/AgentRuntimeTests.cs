using FluentAssertions;
using MicroForge.Domain.Configuration;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.AI;
using MicroForge.Infrastructure.Telemetry;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System.Text.Json.Nodes;

namespace MicroForge.UnitTests;

/// <summary>
/// Unit tests for the AgentRuntime — LLM ↔ Tool invocation loop orchestration.
/// </summary>
public class AgentRuntimeTests
{
    private readonly AgentSettings _settings = new()
    {
        MaxIterations = 5,
        PerToolTimeoutSeconds = 10,
        SessionTimeoutSeconds = 60,
        LlmProvider = "mock",
        LlmModel = "mock-model",
        LlmEndpoint = ""
    };

    private readonly ToolRegistry _registry = new(Enumerable.Empty<ITool>(), NullLogger<ToolRegistry>.Instance);

    private AgentRuntime createruntime(MockLlmClient llmclient)
    {
        return new AgentRuntime(
            _registry,
            llmclient,
            Options.Create(_settings),
            NullLogger<AgentRuntime>.Instance, new AppMetricsService());
    }

    private static AgentRequest createrequest(string message = "Hello")
    {
        return new AgentRequest
        {
            Message = message,
            SessionId = "test-session",
            UserId = 1,
            Username = "testuser",
            Roles = new List<string> { "Admin" },
            ProductionLineIds = new List<int>()
        };
    }

    [Fact]
    public async Task executeasync_with_no_tool_call_returns_response_immediately()
    {
        var llm = new MockLlmClient(new LlmResponse
        {
            Content = "Hello! How can I help you?",
            ToolCall = null,
            FinishReason = true,
            TokensUsed = 50
        });
        var runtime = createruntime(llm);

        var result = await runtime.ExecuteAsync(createrequest());

        result.IsSuccess.Should().BeTrue();
        result.Response.Should().Be("Hello! How can I help you?");
        result.ToolsInvoked.Should().BeEmpty();
        result.TokensUsed.Should().Be(50);
    }

    [Fact]
    public async Task executeasync_with_tool_call_invokes_tool_and_feeds_result_back()
    {
        var tool = new MockTool("mock_tool", "A mock tool", "test");
        _registry.Register(tool);

        var llm = new MockLlmClient(
            new LlmResponse
            {
                Content = "Let me check that.",
                ToolCall = new ToolCall { Name = "mock_tool", Arguments = null },
                FinishReason = false,
                TokensUsed = 30
            },
            new LlmResponse
            {
                Content = "Based on the data, here is your answer.",
                ToolCall = null,
                FinishReason = true,
                TokensUsed = 40
            }
        );
        var runtime = createruntime(llm);

        var result = await runtime.ExecuteAsync(createrequest("check something"));

        result.IsSuccess.Should().BeTrue();
        result.Response.Should().Be("Based on the data, here is your answer.");
        result.ToolsInvoked.Should().ContainSingle().Which.Should().Be("mock_tool");
        tool.wasinvoked.Should().BeTrue();
        result.TokensUsed.Should().Be(70);
    }

    [Fact]
    public async Task executeasync_with_nonexistent_tool_records_error_and_continues()
    {
        var llm = new MockLlmClient(
            new LlmResponse
            {
                Content = "Let me use a tool.",
                ToolCall = new ToolCall { Name = "nonexistent_tool", Arguments = null },
                FinishReason = false,
                TokensUsed = 20
            },
            new LlmResponse
            {
                Content = "Sorry, I couldn't find that tool.",
                ToolCall = null,
                FinishReason = true,
                TokensUsed = 30
            }
        );
        var runtime = createruntime(llm);

        var result = await runtime.ExecuteAsync(createrequest());

        result.IsSuccess.Should().BeTrue();
        result.Response.Should().Be("Sorry, I couldn't find that tool.");
        result.ToolsInvoked.Should().BeEmpty();
    }

    [Fact]
    public async Task executeasync_with_multiple_tool_calls_invokes_all()
    {
        var tool1 = new MockTool("tool_one", "First tool", "test");
        var tool2 = new MockTool("tool_two", "Second tool", "test");
        _registry.Register(tool1);
        _registry.Register(tool2);

        var llm = new MockLlmClient(
            new LlmResponse { Content = "Step 1", ToolCall = new ToolCall { Name = "tool_one" }, FinishReason = false, TokensUsed = 10 },
            new LlmResponse { Content = "Step 2", ToolCall = new ToolCall { Name = "tool_two" }, FinishReason = false, TokensUsed = 10 },
            new LlmResponse { Content = "Done!", ToolCall = null, FinishReason = true, TokensUsed = 10 }
        );
        var runtime = createruntime(llm);

        var result = await runtime.ExecuteAsync(createrequest());

        result.ToolsInvoked.Should().HaveCount(2);
        result.ToolsInvoked.Should().Contain("tool_one");
        result.ToolsInvoked.Should().Contain("tool_two");
        tool1.wasinvoked.Should().BeTrue();
        tool2.wasinvoked.Should().BeTrue();
    }

    [Fact]
    public async Task executeasync_with_tool_exception_records_error_and_continues()
    {
        var tool = new MockTool("failing_tool", "A failing tool", "test", throwexception: true);
        _registry.Register(tool);

        var llm = new MockLlmClient(
            new LlmResponse { Content = "Let me try.", ToolCall = new ToolCall { Name = "failing_tool" }, FinishReason = false, TokensUsed = 10 },
            new LlmResponse { Content = "The tool failed, but here's what I know.", ToolCall = null, FinishReason = true, TokensUsed = 20 }
        );
        var runtime = createruntime(llm);

        var result = await runtime.ExecuteAsync(createrequest());

        result.IsSuccess.Should().BeTrue();
        result.Response.Should().Be("The tool failed, but here's what I know.");
        result.ToolsInvoked.Should().Contain("failing_tool");
    }

    [Fact]
    public async Task executeasync_with_max_iterations_returns_fallback_message()
    {
        // LLM always returns a tool call, never a final answer
        var llm = new MockLlmClient(new LlmResponse
        {
            Content = "Still working...",
            ToolCall = new ToolCall { Name = "mock_tool" },
            FinishReason = false,
            TokensUsed = 10
        });
        var tool = new MockTool("mock_tool", "Tool", "test");
        _registry.Register(tool);

        // Set max iterations to 2
        _settings.MaxIterations = 2;
        var runtime = createruntime(llm);

        var result = await runtime.ExecuteAsync(createrequest());

        result.IsSuccess.Should().BeTrue();
        result.Response.Should().Contain("maximum number of tool iterations");
    }

    [Fact]
    public async Task executeasync_includes_user_info_in_system_prompt()
    {
        var llm = new MockLlmClient(new LlmResponse
        {
            Content = "Response",
            ToolCall = null,
            FinishReason = true,
            TokensUsed = 10
        });
        var runtime = createruntime(llm);

        var request = new AgentRequest
        {
            Message = "Hello",
            SessionId = "test-session",
            UserId = 1,
            Username = "specialuser",
            Roles = new List<string> { "Admin", "Engineer" },
            ProductionLineIds = new List<int>()
        };

        await runtime.ExecuteAsync(request);

        llm.lastsystemprompt.Should().Contain("specialuser");
        llm.lastsystemprompt.Should().Contain("Admin");
        llm.lastsystemprompt.Should().Contain("Engineer");
    }

    [Fact]
    public async Task executeasync_includes_production_line_ids_in_system_prompt()
    {
        var llm = new MockLlmClient(new LlmResponse
        {
            Content = "Response",
            ToolCall = null,
            FinishReason = true,
            TokensUsed = 10
        });
        var runtime = createruntime(llm);

        var request = new AgentRequest
        {
            Message = "Hello",
            SessionId = "test-session",
            UserId = 1,
            Username = "testuser",
            Roles = new List<string> { "Admin" },
            ProductionLineIds = new List<int> { 1, 2, 3 }
        };

        await runtime.ExecuteAsync(request);

        llm.lastsystemprompt.Should().Contain("1, 2, 3");
    }

    [Fact]
    public async Task executeasync_durationms_is_positive()
    {
        var llm = new MockLlmClient(new LlmResponse
        {
            Content = "Quick response",
            ToolCall = null,
            FinishReason = true,
            TokensUsed = 5
        });
        var runtime = createruntime(llm);

        var result = await runtime.ExecuteAsync(createrequest());

        result.DurationMs.Should().BeGreaterThanOrEqualTo(0);
    }

    [Fact]
    public async Task executeasync_returns_messages_in_conversation()
    {
        var llm = new MockLlmClient(new LlmResponse
        {
            Content = "Hello!",
            ToolCall = null,
            FinishReason = true,
            TokensUsed = 5
        });
        var runtime = createruntime(llm);

        var result = await runtime.ExecuteAsync(createrequest("Hi there"));

        result.Messages.Should().NotBeEmpty();
        result.Messages[0].Role.Should().Be("user");
        result.Messages[0].Content.Should().Be("Hi there");
    }

    /// <summary>
    /// Mock LLM client that returns pre-configured responses in sequence.
    /// </summary>
    private class MockLlmClient : ILlmClient
    {
        private readonly Queue<LlmResponse> _responses;
        private LlmResponse? _lastresponse;
        public string lastsystemprompt = string.Empty;

        public MockLlmClient(params LlmResponse[] responses)
        {
            _responses = new Queue<LlmResponse>(responses);
        }

        public Task<LlmResponse> CompleteAsync(LlmRequest request, CancellationToken ct = default)
        {
            lastsystemprompt = request.SystemPrompt;
            LlmResponse response;
            if (_responses.Count > 0)
            {
                response = _responses.Dequeue();
                _lastresponse = response;
            }
            else
            {
                response = _lastresponse ?? new LlmResponse
                {
                    Content = "Fallback response",
                    ToolCall = null,
                    FinishReason = true,
                    TokensUsed = 10
                };
            }
            return Task.FromResult(response);
        }

        public Task<List<string>> GetAvailableModelsAsync(CancellationToken ct = default)
        {
            return Task.FromResult(new List<string> { "mock-model" });
        }
    }

    /// <summary>
    /// Mock tool for agent runtime testing.
    /// </summary>
    private class MockTool : ITool
    {
        public string Name { get; }
        public string Description { get; }
        public string Category { get; }
        public JsonObject Schema => new() { ["type"] = "object" };
        public bool wasinvoked;
        private readonly bool _throwexception;

        public MockTool(string name, string description, string category, bool throwexception = false)
        {
            Name = name;
            Description = description;
            Category = category;
            _throwexception = throwexception;
        }

        public Task<ToolResult> InvokeAsync(JsonObject? arguments, ToolExecutionContext context, CancellationToken ct = default)
        {
            wasinvoked = true;
            if (_throwexception)
                throw new InvalidOperationException("Tool failed on purpose");
            return Task.FromResult(ToolResult.Ok("mock tool output"));
        }
    }
}