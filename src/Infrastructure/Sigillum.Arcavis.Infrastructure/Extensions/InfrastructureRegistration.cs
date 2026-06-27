using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sigillum.Arcavis.Infrastructure.EventBus.Extensions;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Extensions;
using Sigillum.Arcavis.Infrastructure.Persistence.RepoDb.Extensions;
using Sigillum.Arcavis.Infrastructure.Security.Extensions;

namespace Sigillum.Arcavis.Infrastructure.Extensions;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructure(this  IServiceCollection services, IConfiguration configuration)
    {
        services.AddEfCoreRegistration(configuration)
                .AddRepoDbRegistration(configuration)
                .AddRabbitMqRegistration(configuration)
                .AddArgon2Registration();

        return services; 
    }
}
