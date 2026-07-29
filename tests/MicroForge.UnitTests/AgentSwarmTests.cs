using FluentAssertions;
using MicroForge.Application.Services;
using MicroForge.Domain.Configuration;
using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.AI;
using MicroForge.Infrastructure.AI.Tools;
using MicroForge.Infrastructure.Data;
using MicroForge.Infrastructure.Telemetry;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MicroForge.UnitTests;

public class AgentSwarmTests : TestBase
{
    private readonly IOptions<AgentSettings> _settings;
    private readonly AppMetricsService _metrics;

    public AgentSwarmTests()
    {
        _settings = Options.Create(new AgentSettings
        {
            LlmProvider = "mock",
            LlmModel = "mock-model",
            MaxIterations = 3
        });
        _metrics = new AppMetricsService();
    }

    [Fact]
    public async Task swarm_orchestrator_routes_wip_query_to_wip_agent()
    {
        // Arrange
        var db = DbContext;
        var wipService = new WipService(
            new Repository<Lot>(db),
            new Repository<Wafer>(db),
            new Repository<RouteStep>(db),
            new Repository<LotMovementLog>(db),
            new Repository<ToolQualification>(db),
            new Repository<WorkInstruction>(db),
            new Repository<LotChecklistProgress>(db),
            new Repository<ProductionLine>(db)
        );

        // Register tools in registry
        var tools = new List<ITool>
        {
            new QueryWipLotsTool(wipService),
            new GetOeeMetricsTool(new FabExpansionService(
                new Repository<ProductionLine>(db),
                new Repository<ProductionRun>(db),
                new Repository<SensorReading>(db),
                new Repository<DefectRecord>(db),
                new Repository<CapaAction>(db),
                new Repository<EcoRecord>(db),
                new Repository<ShiftLog>(db),
                new Repository<ToolMaintenanceLog>(db),
                new Repository<RecipeVersion>(db)
            ))
        };

        var toolRegistry = new ToolRegistry(tools, NullLogger<ToolRegistry>.Instance);
        var llmClient = new LlmService(new HttpClient(), _settings, NullLogger<LlmService>.Instance);

        var swarmRuntime = new AgentSwarmRuntime(
            toolRegistry,
            llmClient,
            _settings,
            NullLogger<AgentSwarmRuntime>.Instance,
            _metrics
        );

        var request = new AgentRequest
        {
            Message = "What is the status of active WIP lots?",
            Username = "engineer",
            Roles = new List<string> { "Engineer" },
            ProductionLineIds = new List<int>()
        };

        // Act
        var response = await swarmRuntime.ExecuteAsync(request, CancellationToken.None);

        // Assert
        response.IsSuccess.Should().BeTrue();
        response.ToolsInvoked.Should().Contain("delegate_to_wip_agent");
    }

    [Fact]
    public async Task swarm_orchestrator_routes_fdc_query_to_fdc_agent()
    {
        // Arrange
        var db = DbContext;
        var fdcService = new FdcService(
            new Repository<ProductionLine>(db),
            new Repository<SensorReading>(db),
            new Repository<ProductionRun>(db)
        );

        var tools = new List<ITool>
        {
            new QueryFdcDiagnosticsTool(fdcService)
        };

        var toolRegistry = new ToolRegistry(tools, NullLogger<ToolRegistry>.Instance);
        var llmClient = new LlmService(new HttpClient(), _settings, NullLogger<LlmService>.Instance);

        var swarmRuntime = new AgentSwarmRuntime(
            toolRegistry,
            llmClient,
            _settings,
            NullLogger<AgentSwarmRuntime>.Instance,
            _metrics
        );

        var request = new AgentRequest
        {
            Message = "Check if we have any vacuum pressure leaks or FDC faults on the chambers",
            Username = "engineer",
            Roles = new List<string> { "Engineer" }
        };

        // Act
        var response = await swarmRuntime.ExecuteAsync(request, CancellationToken.None);

        // Assert
        response.IsSuccess.Should().BeTrue();
        response.ToolsInvoked.Should().Contain("delegate_to_fdc_agent");
    }
}
