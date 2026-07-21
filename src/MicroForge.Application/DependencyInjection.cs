using MicroForge.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MicroForge.Application;

/// <summary>
/// DI registration extensions for the Application layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers all application-layer services.
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductionService, ProductionService>();
        services.AddScoped<IAgentAppService, AgentAppService>();
        services.AddScoped<IDocumentAppService, DocumentAppService>();
        services.AddScoped<IWipService, WipService>();
        services.AddScoped<IFdcService, FdcService>();
        services.AddScoped<IFabExpansionService, FabExpansionService>();
        return services;
    }
}