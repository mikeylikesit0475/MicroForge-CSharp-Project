using MicroForge.Domain.Entities;
using MicroForge.Domain.Interfaces;
using MicroForge.Infrastructure.Data;
using MicroForge.Infrastructure.Telemetry;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MicroForge.Infrastructure.Events;

public class SensorSimulator : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEventBus _eventBus;
    private readonly IAnomalyDetector _anomalyDetector;
    private readonly ILogger<SensorSimulator> _logger;
    private readonly AppMetricsService _metrics;
    private static readonly Random _random = new();

    public SensorSimulator(
        IServiceProvider serviceProvider,
        IEventBus eventBus,
        IAnomalyDetector anomalyDetector,
        ILogger<SensorSimulator> logger,
        AppMetricsService metrics)
    {
        _serviceProvider = serviceProvider;
        _eventBus = eventBus;
        _anomalyDetector = anomalyDetector;
        _logger = logger;
        _metrics = metrics;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Sensor simulator started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<MicroForgeDbContext>();

                var lines = db.ProductionLines.Where(l => l.IsActive).ToList();
                foreach (var line in lines)
                {
                    var readings = GenerateReadings(line.Id);
                    foreach (var reading in readings)
                    {
                        var recentValues = db.SensorReadings
                            .Where(r => r.ProductionLineId == line.Id && r.SensorType == reading.SensorType)
                            .OrderByDescending(r => r.Timestamp)
                            .Take(10)
                            .Select(r => r.Value)
                            .ToList();

                        var anomaly = _anomalyDetector.Detect(reading.SensorType, reading.Value, reading.Unit, recentValues);
                        reading.IsAnomaly = anomaly.IsAnomaly;
                        reading.AnomalyType = anomaly.AnomalyType;

                        db.SensorReadings.Add(reading);

                        if (anomaly.IsAnomaly)
                        {
                            var alert = new Alert
                            {
                                ProductionLineId = line.Id,
                                AlertType = anomaly.AnomalyType ?? "Anomaly",
                                Severity = anomaly.AnomalyType == "HighThreshold" ? "Critical" : "Warning",
                                Title = $"{reading.SensorType} Anomaly on {line.Name}",
                                Message = anomaly.Description ?? "Anomaly detected",
                                Source = "SensorSimulator",
                                IsAcknowledged = false,
                                CreatedAt = DateTime.UtcNow
                            };
                            db.Alerts.Add(alert);
                        }
                    }

                    await db.SaveChangesAsync(stoppingToken);
                    _metrics.IncrementPipelineEventsProcessed();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in sensor simulator cycle");
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }

    private static List<SensorReading> GenerateReadings(int lineId)
    {
        var readings = new List<SensorReading>();
        var now = DateTime.UtcNow;

        var temp = 25m + (decimal)(_random.NextDouble() * 10 - 5);
        if (_random.NextDouble() < 0.05) temp = 90m + (decimal)(_random.NextDouble() * 10);
        readings.Add(new SensorReading
        {
            ProductionLineId = lineId, SensorType = "Temperature",
            Value = Math.Round(temp, 2), Unit = "°C", Timestamp = now
        });

        var pressure = 2.0m + (decimal)(_random.NextDouble() * 1 - 0.5);
        if (_random.NextDouble() < 0.05) pressure = 0.1m;
        readings.Add(new SensorReading
        {
            ProductionLineId = lineId, SensorType = "Pressure",
            Value = Math.Round(pressure, 2), Unit = "bar", Timestamp = now
        });

        var vibration = 3.0m + (decimal)(_random.NextDouble() * 2 - 1);
        if (_random.NextDouble() < 0.05) vibration = 12m;
        readings.Add(new SensorReading
        {
            ProductionLineId = lineId, SensorType = "Vibration",
            Value = Math.Round(vibration, 2), Unit = "mm/s", Timestamp = now
        });

        var humidity = 45m + (decimal)(_random.NextDouble() * 10 - 5);
        readings.Add(new SensorReading
        {
            ProductionLineId = lineId, SensorType = "Humidity",
            Value = Math.Round(humidity, 2), Unit = "%", Timestamp = now
        });

        return readings;
    }
}