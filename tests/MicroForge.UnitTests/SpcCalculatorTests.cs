using FluentAssertions;
using MicroForge.Infrastructure.Analytics;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MicroForge.UnitTests;

public class SpcCalculatorTests
{
    [Fact]
    public void evaluate_calculates_correct_mean_and_stddev()
    {
        // Arrange
        var history = new List<decimal> { 10m, 12m, 11m, 9m };
        var currentValue = 13m; // Combined: 10, 12, 11, 9, 13

        // Act
        var result = SpcCalculator.Evaluate(currentValue, history);

        // Assert
        result.Mean.Should().Be(10.5m);
        result.StdDev.Should().BeApproximately(1.1180m, 0.001m);
    }

    [Fact]
    public void rule1_triggers_when_point_exceeds_3sigma()
    {
        // Arrange
        var history = new List<decimal> { 10m, 10m, 10m, 10m };
        var currentValue = 50m; // Outlier

        // Act
        var result = SpcCalculator.Evaluate(currentValue, history);

        // Assert
        result.IsViolation.Should().BeTrue();
        result.Violations.Should().Contain(v => v.Contains("Rule 1"));
    }

    [Fact]
    public void rule2_triggers_when_9_points_on_same_side_of_mean()
    {
        // Arrange
        var data = new List<decimal> { 5m, 5m, 5m, 5m, 20m, 20m, 20m, 20m, 20m, 20m, 20m, 20m, 20m };
        var historyList = data.Take(12).ToList();
        var currentVal = data.Last();

        // Act
        var result = SpcCalculator.Evaluate(currentVal, historyList);

        // Assert
        result.IsViolation.Should().BeTrue();
        result.Violations.Should().Contain(v => v.Contains("Rule 2"));
    }

    [Fact]
    public void rule3_triggers_on_6_consecutive_increasing_points()
    {
        // Arrange
        var history = new List<decimal> { 1m, 2m, 3m, 4m, 5m };
        var currentValue = 6m;

        // Act
        var result = SpcCalculator.Evaluate(currentValue, history);

        // Assert
        result.IsViolation.Should().BeTrue();
        result.Violations.Should().Contain(v => v.Contains("Rule 3") && v.Contains("increasing"));
    }

    [Fact]
    public void rule3_triggers_on_6_consecutive_decreasing_points()
    {
        // Arrange
        var history = new List<decimal> { 10m, 9m, 8m, 7m, 6m };
        var currentValue = 5m;

        // Act
        var result = SpcCalculator.Evaluate(currentValue, history);

        // Assert
        result.IsViolation.Should().BeTrue();
        result.Violations.Should().Contain(v => v.Contains("Rule 3") && v.Contains("decreasing"));
    }
}
