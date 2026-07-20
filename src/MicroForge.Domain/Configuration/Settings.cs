namespace MicroForge.Domain.Configuration;

/// <summary>
/// JWT configuration settings loaded from appsettings.json.
/// </summary>
public class JwtSettings
{
    public string Issuer { get; set; } = "MicroForge";
    public string Audience { get; set; } = "MicroForge-Client";
    public string SecretKey { get; set; } = string.Empty;
    public int AccessTokenExpiryMinutes { get; set; } = 60;
    public int RefreshTokenExpiryDays { get; set; } = 7;
}

/// <summary>
/// RAG pipeline configuration.
/// </summary>
public class RagSettings
{
    public int ChunkSizeTokens { get; set; } = 500;
    public int ChunkOverlapTokens { get; set; } = 50;
    public int TopK { get; set; } = 5;
    public int EmbeddingDimension { get; set; } = 384;
}

/// <summary>
/// Agent runtime configuration.
/// </summary>
public class AgentSettings
{
    public int MaxIterations { get; set; } = 10;
    public int PerToolTimeoutSeconds { get; set; } = 30;
    public int SessionTimeoutSeconds { get; set; } = 120;
    public string LlmProvider { get; set; } = "mock";
    public string LlmApiKey { get; set; } = string.Empty;
    public string LlmModel { get; set; } = "mock-model";
    public string LlmEndpoint { get; set; } = string.Empty;
}

/// <summary>
/// Sensor simulation configuration.
/// </summary>
public class SensorSettings
{
    public int IntervalSeconds { get; set; } = 5;
    public bool AutoStart { get; set; } = true;
}