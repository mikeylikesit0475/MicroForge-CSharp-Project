# ─── Build stage ─────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY MicroForge.sln .
COPY src/MicroForge.Domain/MicroForge.Domain.csproj src/MicroForge.Domain/MicroForge.Domain.csproj
COPY src/MicroForge.Application/MicroForge.Application.csproj src/MicroForge.Application/MicroForge.Application.csproj
COPY src/MicroForge.Infrastructure/MicroForge.Infrastructure.csproj src/MicroForge.Infrastructure/MicroForge.Infrastructure.csproj
COPY src/MicroForge.Web/MicroForge.Web.csproj src/MicroForge.Web/MicroForge.Web.csproj
COPY tests/MicroForge.UnitTests/MicroForge.UnitTests.csproj tests/MicroForge.UnitTests/MicroForge.UnitTests.csproj
COPY tests/MicroForge.IntegrationTests/MicroForge.IntegrationTests.csproj tests/MicroForge.IntegrationTests/MicroForge.IntegrationTests.csproj
COPY tests/MicroForge.AgentTests/MicroForge.AgentTests.csproj tests/MicroForge.AgentTests/MicroForge.AgentTests.csproj
RUN dotnet restore

COPY src/ src/
COPY tests/ tests/
RUN dotnet build MicroForge.sln -c Release --no-restore
RUN dotnet publish src/MicroForge.Web/MicroForge.Web.csproj -c Release -o /app/publish --no-restore

# ─── Runtime stage ──────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

RUN mkdir -p /app/data /app/logs

COPY --from=build /app/publish .

# Run as non-root user for security
RUN adduser --disabled-password --gecos "" --uid 1000 appuser && \
    chown -R appuser:appuser /app
USER appuser

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:5000
ENV ConnectionStrings__DefaultConnection="Data Source=/app/data/microforge.db"

EXPOSE 5000

HEALTHCHECK --interval=30s --timeout=5s --retries=3 \
    CMD curl -f http://localhost:5000/health || exit 1

ENTRYPOINT ["dotnet", "MicroForge.Web.dll"]