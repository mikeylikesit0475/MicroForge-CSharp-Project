using MicroForge.Domain.Configuration;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.AI;
using MicroForge.Infrastructure.AI.Tools;
using MicroForge.Infrastructure.Auth;
using MicroForge.Infrastructure.Cache;
using MicroForge.Infrastructure.Data;
using MicroForge.Infrastructure.Events;
using MicroForge.Infrastructure.Rag;
using MicroForge.Infrastructure.Telemetry;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroForge.Infrastructure;

/// <summary>
/// DI registration extensions for the Infrastructure layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers all infrastructure services: DbContext, repositories, auth, AI, RAG, events, caching.
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        services.AddDbContext<MicroForgeDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")
                ?? "Data Source=microforge.db"));

        // Generic repository
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // Configuration
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
        services.Configure<RagSettings>(configuration.GetSection("Rag"));
        services.Configure<AgentSettings>(configuration.GetSection("Agent"));
        services.Configure<SensorSettings>(configuration.GetSection("Sensor"));

        // Register JwtSettings as a singleton for direct injection
        var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>() ?? new JwtSettings();
        services.AddSingleton(jwtSettings);

        // Auth
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IDataBoundaryPolicy, DataBoundaryPolicy>();

        // Cache
        services.AddMemoryCache();
        services.AddSingleton<ICacheService, CacheService>();

        // Telemetry — singleton metrics counters (MF-034)
        services.AddSingleton<AppMetricsService>();

        // Events
        services.AddSingleton<IEventBus, EventBus>();
        services.AddSingleton<IAnomalyDetector, AnomalyDetector>();

        // RAG
        services.AddScoped<IDocumentChunker, DocumentChunker>();
        services.AddSingleton<IEmbeddingService, EmbeddingService>();
        services.AddSingleton<IVectorStore, VectorStore>();
        services.AddScoped<IDocumentService, DocumentService>();

        // AI Agent
        services.AddScoped<IToolRegistry, ToolRegistry>();
        services.AddScoped<AgentRuntime>();
        services.AddScoped<IAgentRuntime, AgentSwarmRuntime>();
        services.AddHttpClient<ILlmClient, LlmService>();

        // Tools — registered as scoped because they depend on DbContext
        services.AddScoped<ITool, QueryProductionDefectsTool>();
        services.AddScoped<ITool, QuerySensorReadingsTool>();
        services.AddScoped<ITool, GetProductionLineStatusTool>();
        services.AddScoped<ITool, GetProductionRunSummaryTool>();
        services.AddScoped<ITool, SearchDocumentsTool>();
        services.AddScoped<ITool, CalculateStatisticsTool>();
        services.AddScoped<ITool, DetectAnomaliesTool>();
        services.AddScoped<ITool, CreateAlertTool>();
        services.AddScoped<ITool, GetYieldTrendTool>();
        services.AddScoped<ITool, QueryAlertsTool>();
        services.AddScoped<ITool, AcknowledgeAlertTool>();
        services.AddScoped<ITool, CreateDefectRecordTool>();
        services.AddScoped<ITool, QueryProductionRunsTool>();
        services.AddScoped<ITool, UpdateProductionRunStatusTool>();
        services.AddScoped<ITool, GetDashboardSummaryTool>();
        services.AddScoped<ITool, QueryWipLotsTool>();
        services.AddScoped<ITool, MoveWipLotTool>();
        services.AddScoped<ITool, HoldWipLotTool>();
        services.AddScoped<ITool, ReleaseWipLotTool>();
        services.AddScoped<ITool, GetOeeMetricsTool>();
        services.AddScoped<ITool, QueryFdcDiagnosticsTool>();
        services.AddScoped<ITool, ManageCapaActionsTool>();
        services.AddScoped<ITool, GetSecsGemLogsTool>();
        services.AddScoped<ITool, ManageQualificationsTool>();
        services.AddScoped<ITool, QueryInventoryTool>();
        services.AddScoped<ITool, ManagePurchaseOrdersTool>();
        services.AddScoped<ITool, GetMrpRecommendationsTool>();
        services.AddScoped<ITool, PredictYieldTool>();
        services.AddScoped<ITool, TriggerSelfHealingTool>();
        services.AddScoped<ITool, AnalyzeRunTraceWithRagTool>();
        services.AddScoped<ITool, GetYieldTrendWithPredictionTool>();
        services.AddScoped<ITool, GetEwiChecklistTool>();
        services.AddScoped<ITool, CompleteEwiChecklistTool>();
        services.AddScoped<ITool, ManageToolMaintenanceTool>();
        services.AddScoped<ITool, CalculateSpcMetricsTool>();

        // Background services
        if (configuration.GetSection("Sensor").Get<SensorSettings>()?.AutoStart != false)
        {
            services.AddHostedService<SensorSimulator>();
        }
        services.AddHostedService<SecsGemSimulator>();

        return services;
    }
}