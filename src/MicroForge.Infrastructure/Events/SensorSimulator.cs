using MicroForge.Domain.Entities;
using MicroForge.Domain.Events;
using MicroForge.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using MicroForge.Infrastructure.Analytics;
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
                            .Take(20)
                            .Select(r => r.Value)
                            .ToList();

                        // 1. Threshold Anomaly check
                        var anomaly = _anomalyDetector.Detect(reading.SensorType, reading.Value, reading.Unit, recentValues.Take(10).ToList());

                        // 2. Statistical Process Control (SPC) rules evaluation
                        var historyAsc = recentValues.AsEnumerable().Reverse().ToList();
                        var spcResult = SpcCalculator.Evaluate(reading.Value, historyAsc);

                        // Save calculated limits on the reading record
                        reading.Mean = spcResult.Mean > 0 ? spcResult.Mean : null;
                        reading.Ucl = spcResult.Ucl > 0 ? spcResult.Ucl : null;
                        reading.Lcl = spcResult.Lcl > 0 ? spcResult.Lcl : null;

                        // Combine anomaly results
                        if (anomaly.IsAnomaly)
                        {
                            reading.IsAnomaly = true;
                            reading.AnomalyType = anomaly.AnomalyType;
                        }
                        else if (spcResult.IsViolation)
                        {
                            reading.IsAnomaly = true;
                            reading.AnomalyType = "SPC_Violation";
                        }

                        db.SensorReadings.Add(reading);

                        // Publish new sensor reading event in-memory
                        await _eventBus.PublishAsync(new SensorReadingEvent
                        {
                            ProductionLineId = reading.ProductionLineId,
                            SensorType = reading.SensorType,
                            Value = reading.Value,
                            Unit = reading.Unit,
                            Timestamp = reading.Timestamp,
                            IsAnomaly = reading.IsAnomaly,
                            AnomalyType = reading.AnomalyType,
                            Mean = reading.Mean,
                            Ucl = reading.Ucl,
                            Lcl = reading.Lcl
                        }, stoppingToken);

                        // Raise alerts on violations
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

                            await _eventBus.PublishAsync(new AlertEvent
                            {
                                ProductionLineId = alert.ProductionLineId,
                                AlertType = alert.AlertType,
                                Severity = alert.Severity,
                                Title = alert.Title,
                                Message = alert.Message,
                                CreatedAt = alert.CreatedAt
                            }, stoppingToken);

                            // AUTO-DISPOSITIONING: Auto-Hold any active lot on this production line
                            if (alert.Severity == "Critical")
                            {
                                var activeMovement = await db.LotMovementLogs
                                    .Include(m => m.Lot)
                                    .Include(m => m.RouteStep)
                                    .FirstOrDefaultAsync(m => m.ExitedAt == null && 
                                        (m.RouteStep!.StepName.ToLower().Contains(line.LineType.ToLower()) || 
                                         line.LineType.ToLower().Contains(m.RouteStep.StepName.ToLower())), stoppingToken);

                                if (activeMovement?.Lot != null && activeMovement.Lot.Status == LotStatus.Active)
                                {
                                    activeMovement.Lot.Status = LotStatus.Hold;
                                    db.Lots.Update(activeMovement.Lot);
                                    
                                    await _eventBus.PublishAsync(new AlertEvent
                                    {
                                        ProductionLineId = line.Id,
                                        AlertType = "FDC_Autodisposition",
                                        Severity = "Critical",
                                        Title = $"Auto-Hold Locked Lot {activeMovement.Lot.LotNumber}",
                                        Message = $"Lot placed on quarantine hold due to critical {reading.SensorType} anomaly.",
                                        CreatedAt = DateTime.UtcNow
                                    }, stoppingToken);
                                }
                            }
                        }
                        else if (spcResult.IsViolation)
                        {
                            var alert = new Alert
                            {
                                ProductionLineId = line.Id,
                                AlertType = "SPC_Violation",
                                Severity = "Warning",
                                Title = $"SPC Rule Violation on {line.Name} ({reading.SensorType})",
                                Message = string.Join("; ", spcResult.Violations),
                                Source = "SensorSimulator",
                                IsAcknowledged = false,
                                CreatedAt = DateTime.UtcNow
                            };
                            db.Alerts.Add(alert);

                            await _eventBus.PublishAsync(new AlertEvent
                            {
                                ProductionLineId = alert.ProductionLineId,
                                AlertType = alert.AlertType,
                                Severity = alert.Severity,
                                Title = alert.Title,
                                Message = alert.Message,
                                CreatedAt = alert.CreatedAt
                            }, stoppingToken);
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