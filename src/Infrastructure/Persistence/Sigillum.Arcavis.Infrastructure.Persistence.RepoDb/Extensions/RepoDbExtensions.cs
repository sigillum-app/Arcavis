using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepoDb;
using Sigillum.Arcavis.Core.Application.Abstraction.Persistence.QueryServices;
using Sigillum.Arcavis.Infrastructure.Persistence.RepoDb.Connection;
using Sigillum.Arcavis.Infrastructure.Persistence.RepoDb.Mapping;
using Sigillum.Arcavis.Infrastructure.Persistence.RepoDb.QueryServices;

namespace Sigillum.Arcavis.Infrastructure.Persistence.RepoDb.Extensions;

public static class RepoDbExtensions
{
    public static IServiceCollection AddRepoDbRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        GlobalConfiguration
            .Setup()
            .UsePostgreSql();

        UserRomMap.Configure();


        services.AddScoped<IUserQueryService, UserQueryService>();

        var connectionString = configuration.GetConnectionString("PostgreSqlConnection");

        services.AddSingleton(new ArcavisConnectionFactory(connectionString));

        return services;
    }
}
