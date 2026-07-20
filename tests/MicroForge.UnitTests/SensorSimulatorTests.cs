using FluentAssertions;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.Events;
using MicroForge.Infrastructure.Telemetry;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Abstractions;

namespace MicroForge.UnitTests;

/// <summary>
/// Unit tests for the SensorSimulator background service — verifies DI registration and lifecycle.
/// </summary>
public class SensorSimulatorTests
{
    [Fact]
    public void sensorsimulator_can_be_constructed_with_required_dependencies()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        var provider = services.BuildServiceProvider();

        var act = () =>
        {
            var simulator = new SensorSimulator(
                provider,
                new EventBus(NullLogger<EventBus>.Instance),
                new AnomalyDetector(),
                NullLogger<SensorSimulator>.Instance, new AppMetricsService());
            return simulator;
        };

        act.Should().NotThrow();
    }

    [Fact]
    public void sensorsimulator_is_a_backgroundservice()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        var provider = services.BuildServiceProvider();

        var simulator = new SensorSimulator(
            provider,
            new EventBus(NullLogger<EventBus>.Instance),
            new AnomalyDetector(),
            NullLogger<SensorSimulator>.Instance, new AppMetricsService());

        simulator.Should().BeAssignableTo<BackgroundService>();
    }

    [Fact]
    public void sensorsimulator_can_be_registered_as_hosted_service()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSingleton<IEventBus, EventBus>();
        services.AddSingleton<IAnomalyDetector, AnomalyDetector>();
        services.AddSingleton<AppMetricsService>();
        services.AddHostedService<SensorSimulator>();

        var provider = services.BuildServiceProvider();

        var hostedServices = provider.GetServices<IHostedService>();
        hostedServices.Should().NotBeEmpty();
    }
}