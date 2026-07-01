using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepoDb;
using Sigillum.Arcavis.Core.Application.Contracts.Persistence.QueryServices;
using Sigillum.Arcavis.Infrastructure.Persistence.RepoDb.Connection;
using Sigillum.Arcavis.Infrastructure.Persistence.RepoDb.Mapping.Base;
using Sigillum.Arcavis.Infrastructure.Persistence.RepoDb.QueryServices;

namespace Sigillum.Arcavis.Infrastructure.Persistence.RepoDb;

public static class RepoDbRegistration
{
    public static IServiceCollection AddRepoDb(this IServiceCollection services, IConfiguration configuration)
    {
        GlobalConfiguration
            .Setup()
            .UsePostgreSql();

        UserRomMap.Configure();
        OutboxRomMap.Configure();

        services.AddScoped<IUserQueryService, UserQueryService>();
        services.AddScoped<IOutboxQueryService, OutboxQueryService>();

        var connectionString = configuration.GetConnectionString("PostgreSqlConnection");

        services.AddSingleton(new ArcavisConnectionFactory(connectionString));

        return services;
    }
}
