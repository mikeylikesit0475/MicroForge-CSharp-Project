using System.Threading;

namespace MicroForge.Infrastructure.Telemetry;

/// <summary>
/// Singleton service that tracks application-level runtime metrics
/// for the Prometheus-style /metrics endpoint (MF-034).
/// All counters are thread-safe using Interlocked operations.
/// </summary>
public sealed class AppMetricsService
{
    private long _httpRequestsTotal;
    private long _httpErrorsTotal;
    private long _signalrConnectionsActive;
    private long _pipelineEventsProcessedTotal;
    private long _agentToolInvocationsTotal;

    /// <summary>Total number of HTTP requests received since process start.</summary>
    public long HttpRequestsTotal => Interlocked.Read(ref _httpRequestsTotal);

    /// <summary>Total number of HTTP responses with status code >= 500.</summary>
    public long HttpErrorsTotal => Interlocked.Read(ref _httpErrorsTotal);

    /// <summary>Current number of active SignalR connections.</summary>
    public long SignalrConnectionsActive => Interlocked.Read(ref _signalrConnectionsActive);

    /// <summary>Total number of pipeline events processed since process start.</summary>
    public long PipelineEventsProcessedTotal => Interlocked.Read(ref _pipelineEventsProcessedTotal);

    /// <summary>Total number of agent tool invocations since process start.</summary>
    public long AgentToolInvocationsTotal => Interlocked.Read(ref _agentToolInvocationsTotal);

    /// <summary>Increment the HTTP request counter by one.</summary>
    public void IncrementHttpRequests() => Interlocked.Increment(ref _httpRequestsTotal);

    /// <summary>Increment the HTTP error counter by one.</summary>
    public void IncrementHttpErrors() => Interlocked.Increment(ref _httpErrorsTotal);

    /// <summary>Increment the active SignalR connection count by one.</summary>
    public void IncrementSignalrConnections() => Interlocked.Increment(ref _signalrConnectionsActive);

    /// <summary>Decrement the active SignalR connection count by one.</summary>
    public void DecrementSignalrConnections() => Interlocked.Decrement(ref _signalrConnectionsActive);

    /// <summary>Increment the pipeline events processed counter by one.</summary>
    public void IncrementPipelineEventsProcessed() => Interlocked.Increment(ref _pipelineEventsProcessedTotal);

    /// <summary>Increment the agent tool invocations counter by one.</summary>
    public void IncrementAgentToolInvocations() => Interlocked.Increment(ref _agentToolInvocationsTotal);
}