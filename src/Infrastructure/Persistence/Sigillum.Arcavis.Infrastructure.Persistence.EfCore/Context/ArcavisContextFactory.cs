using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Context;

public sealed class ArcavisContextFactory : IDesignTimeDbContextFactory<ArcavisContext>
{
    public ArcavisContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ArcavisContext>();

        optionsBuilder.UseNpgsql(
            configuration.GetConnectionString("PostgreSqlConnection"));

        return new ArcavisContext(optionsBuilder.Options);
    }
}