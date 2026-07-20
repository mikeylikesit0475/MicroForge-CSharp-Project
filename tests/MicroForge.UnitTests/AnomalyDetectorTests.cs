using FluentAssertions;
using MicroForge.Infrastructure.Events;

namespace MicroForge.UnitTests;

/// <summary>
/// Unit tests for the AnomalyDetector — threshold-based and statistical outlier detection.
/// </summary>
public class AnomalyDetectorTests
{
    private readonly AnomalyDetector _detector = new();

    [Fact]
    public void detect_high_temperature_above_threshold_returns_highthreshold_anomaly()
    {
        var result = _detector.Detect("Temperature", 90m, "°C", new List<decimal> { 25, 26, 24, 25, 27 });

        result.IsAnomaly.Should().BeTrue();
        result.AnomalyType.Should().Be("HighThreshold");
        result.Threshold.Should().Be(85m);
    }

    [Fact]
    public void detect_low_temperature_below_threshold_returns_lowthreshold_anomaly()
    {
        var result = _detector.Detect("Temperature", 10m, "°C", new List<decimal> { 25, 26, 24, 25, 27 });

        result.IsAnomaly.Should().BeTrue();
        result.AnomalyType.Should().Be("LowThreshold");
        result.Threshold.Should().Be(15m);
    }

    [Fact]
    public void detect_normal_temperature_returns_no_anomaly()
    {
        var result = _detector.Detect("Temperature", 25m, "°C", new List<decimal> { 25, 26, 24, 25, 27 });

        result.IsAnomaly.Should().BeFalse();
    }

    [Fact]
    public void detect_high_pressure_above_threshold_returns_anomaly()
    {
        var result = _detector.Detect("Pressure", 6.0m, "bar", new List<decimal> { 2, 2.1m, 1.9m, 2.0m, 2.1m });

        result.IsAnomaly.Should().BeTrue();
        result.AnomalyType.Should().Be("HighThreshold");
    }

    [Fact]
    public void detect_low_pressure_below_threshold_returns_anomaly()
    {
        var result = _detector.Detect("Pressure", 0.3m, "bar", new List<decimal> { 2, 2.1m, 1.9m, 2.0m, 2.1m });

        result.IsAnomaly.Should().BeTrue();
        result.AnomalyType.Should().Be("LowThreshold");
    }

    [Fact]
    public void detect_high_vibration_above_threshold_returns_anomaly()
    {
        var result = _detector.Detect("Vibration", 12m, "mm/s", new List<decimal> { 3, 4, 3, 3.5m, 4 });

        result.IsAnomaly.Should().BeTrue();
        result.AnomalyType.Should().Be("HighThreshold");
    }

    [Fact]
    public void detect_humidity_within_range_returns_no_anomaly()
    {
        var result = _detector.Detect("Humidity", 50m, "%", new List<decimal> { 45, 50, 48, 52, 49 });

        result.IsAnomaly.Should().BeFalse();
    }

    [Fact]
    public void detect_unknown_sensor_type_with_fewer_than_5_values_returns_no_anomaly()
    {
        var result = _detector.Detect("FlowRate", 100m, "L/min", new List<decimal> { 10, 11, 10 });

        result.IsAnomaly.Should().BeFalse();
    }

    [Fact]
    public void detect_statistical_outlier_with_zscore_above_3_returns_anomaly()
    {
        // Mean ~ 50, std ~ 0, one value at 200 should be a massive outlier
        var recentvalues = new List<decimal> { 50, 50, 50, 50, 50, 50, 50, 50, 50, 50 };

        var result = _detector.Detect("FlowRate", 200m, "L/min", recentvalues);

        result.IsAnomaly.Should().BeTrue();
        result.AnomalyType.Should().Be("StatisticalOutlier");
        result.ZScore.Should().NotBeNull();
        Math.Abs(result.ZScore!.Value).Should().BeGreaterThan(3m);
    }

    [Fact]
    public void detect_normal_value_with_low_variance_returns_no_anomaly()
    {
        var recentvalues = new List<decimal> { 50, 50, 50, 50, 50 };

        var result = _detector.Detect("FlowRate", 50m, "L/min", recentvalues);

        result.IsAnomaly.Should().BeFalse();
    }

    [Fact]
    public void detect_threshold_check_takes_precedence_over_statistical()
    {
        // Temperature 90 is above max threshold (85), so it should return HighThreshold not StatisticalOutlier
        var recentvalues = new List<decimal> { 25, 25, 25, 25, 25, 25, 25, 25, 25, 25 };

        var result = _detector.Detect("Temperature", 90m, "°C", recentvalues);

        result.IsAnomaly.Should().BeTrue();
        result.AnomalyType.Should().Be("HighThreshold");
    }

    [Fact]
    public void detect_description_contains_sensor_type_and_value()
    {
        var result = _detector.Detect("Temperature", 90m, "°C", new List<decimal> { 25, 26, 24, 25, 27 });

        result.Description.Should().Contain("Temperature");
        result.Description.Should().Contain("90");
    }
}