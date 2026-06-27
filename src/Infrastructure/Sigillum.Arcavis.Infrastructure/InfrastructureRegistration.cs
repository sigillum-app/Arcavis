using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sigillum.Arcavis.Infrastructure.EventBus;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore;
using Sigillum.Arcavis.Infrastructure.Persistence.RepoDb;
using Sigillum.Arcavis.Infrastructure.Security;

namespace Sigillum.Arcavis.Infrastructure;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructure(this  IServiceCollection services, IConfiguration configuration)
    {
        services.AddEfCore(configuration)
                .AddRepoDb(configuration)
                .AddRabbitMQ(configuration)
                .AddArgon2();

        return services; 
    }
}
