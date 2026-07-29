using FluentAssertions;
using MicroForge.Application.Dtos;
using MicroForge.Application.Services;
using MicroForge.Domain.Entities;
using MicroForge.Infrastructure.AI.Tools;
using MicroForge.Infrastructure.Data;
using Moq;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MicroForge.UnitTests;

public class MultivariateRcaTests : TestBase
{
    [Fact]
    public async Task multivariate_rca_service_generates_trace_diff_and_sop_citations()
    {
        // Arrange
        var db = DbContext;
        var lineRepo = new Repository<ProductionLine>(db);
        var runRepo = new Repository<ProductionRun>(db);
        var mockDocAppService = new Mock<IDocumentAppService>();

        mockDocAppService.Setup(s => s.SearchAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<DocumentSearchResultDto>
            {
                new DocumentSearchResultDto { Content = "SOP-ETCH-882 helium line leak check clause", Similarity = 0.94 }
            });

        var line = new ProductionLine { Name = "Line-RCA-1", Location = "Fab-1", LineType = "Etch", IsActive = true };
        await lineRepo.AddAsync(line, CancellationToken.None);
        await lineRepo.SaveChangesAsync(CancellationToken.None);

        var run = new ProductionRun
        {
            ProductionLineId = line.Id,
            BatchNumber = "RUN-RCA-500",
            Status = "Completed"
        };
        await runRepo.AddAsync(run, CancellationToken.None);
        await runRepo.SaveChangesAsync(CancellationToken.None);

        var service = new MultivariateRcaService(mockDocAppService.Object, runRepo);

        // Act
        var result = await service.GetMultivariateRcaAsync(run.Id, "RF_Power", CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.RunId.Should().Be(run.Id);
        result.LotNumber.Should().Be("RUN-RCA-500");
        result.TracePoints.Should().HaveCount(31);
        result.OutlierPointCount.Should().BeGreaterThan(0);
        result.MaxDeviationSigma.Should().BeGreaterThanOrEqualTo(3.0m);
        result.SopCitation.Should().NotBeNull();
        result.SopCitation.SectionClause.Should().Contain("Clause 4.2");
    }

    [Fact]
    public async Task analyze_run_trace_with_rag_tool_executes_successfully()
    {
        // Arrange
        var db = DbContext;
        var lineRepo = new Repository<ProductionLine>(db);
        var runRepo = new Repository<ProductionRun>(db);
        var mockDocAppService = new Mock<IDocumentAppService>();
        mockDocAppService.Setup(s => s.SearchAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<DocumentSearchResultDto>
            {
                new DocumentSearchResultDto { Content = "SOP-ETCH-882 clause snippet", Similarity = 0.92 }
            });

        var line = new ProductionLine { Name = "Line-RCA-2", Location = "Fab-1", LineType = "Etch", IsActive = true };
        await lineRepo.AddAsync(line, CancellationToken.None);
        await lineRepo.SaveChangesAsync(CancellationToken.None);

        var run = new ProductionRun { ProductionLineId = line.Id, BatchNumber = "RUN-TOOL-RCA", Status = "Completed" };
        await runRepo.AddAsync(run, CancellationToken.None);
        await runRepo.SaveChangesAsync(CancellationToken.None);

        var service = new MultivariateRcaService(mockDocAppService.Object, runRepo);
        var tool = new AnalyzeRunTraceWithRagTool(service);

        // Act
        var result = await tool.InvokeAsync(new JsonObject { ["runId"] = run.Id, ["sensorType"] = "Chamber_Pressure" }, new MicroForge.Domain.Interfaces.ToolExecutionContext { Username = "engineer" }, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain("RUN-TOOL-RCA");
        result.Output.Should().Contain("SOP-ETCH-882");
    }

    [Fact]
    public async Task rca_rag_citation_auto_remediation_triggers_closed_loop_self_healing()
    {
        // Arrange
        var db = DbContext;
        var lotRepo = new Repository<Lot>(db);
        var stepRepo = new Repository<RouteStep>(db);
        var moveRepo = new Repository<LotMovementLog>(db);
        var capaRepo = new Repository<CapaAction>(db);
        var ecoRepo = new Repository<EcoRecord>(db);

        var step = new RouteStep { StepName = "WAFER_PREP", SequenceOrder = 1 };
        await stepRepo.AddAsync(step, CancellationToken.None);
        await stepRepo.SaveChangesAsync(CancellationToken.None);

        var lot = new Lot { LotNumber = "LOT-RCA-SOP-AUTO", RecipeName = "DRAM-16Gb", Status = LotStatus.Active, ActiveRouteStepId = step.Id };
        await lotRepo.AddAsync(lot, CancellationToken.None);
        await lotRepo.SaveChangesAsync(CancellationToken.None);

        var selfHealingService = new FabSelfHealingService(lotRepo, stepRepo, moveRepo, capaRepo, ecoRepo);

        // Act - Trigger remediation using the RCA RAG citation SOP anomaly text
        var anomaly = "SOP-ETCH-882 Clause 4.2: RF Power Reflection +3.0σ Deviation";
        var result = await selfHealingService.TriggerSelfHealingAsync(lot.Id, anomaly, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.LotNumber.Should().Be("LOT-RCA-SOP-AUTO");
        result.NewStatus.Should().Be("Hold");
        result.AnomalyDescription.Should().Contain("SOP-ETCH-882");
        result.CapaId.Should().BeGreaterThan(0);
        result.EcoId.Should().BeGreaterThan(0);
        result.RemediationSummary.Should().Contain("Autonomous Swarm quarantined");
    }
}
