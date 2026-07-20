using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MicroForge.Infrastructure.Data;

/// <summary>
/// Design-time factory for EF Core migrations — allows `dotnet ef migrations` to run
/// without booting the full web application.
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MicroForgeDbContext>
{
    public MicroForgeDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<MicroForgeDbContext>()
            .UseSqlite("Data Source=microforge.db")
            .Options;
        return new MicroForgeDbContext(options);
    }
}