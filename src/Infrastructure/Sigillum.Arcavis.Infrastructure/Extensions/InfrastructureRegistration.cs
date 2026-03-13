using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sigillum.Arcavis.Infrastructure.Dispatchers.MediatR;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Extensions;
using Sigillum.Arcavis.Infrastructure.Security.Extensions;
using Sigillum.Arcavis.Infrastructure.Persistence.RepoDb.Extensions;
using Sigillum.Arcavis.Core.Application.Abstraction.Dispatcher;
using Sigillum.Arcavis.Infrastructure.EventBus.Extensions;

namespace Sigillum.Arcavis.Infrastructure.Extensions;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructure(this  IServiceCollection services, IConfiguration configuration)
    {
        services.AddEfCoreRegistration(configuration)
                .AddRepoDbRegistration(configuration)
                .AddEventBusRegistration(configuration)
                .AddArgon2Registration();

        #region Dispatchers
        services
            .AddScoped<ICommandDispatcher, CommandDispatcher>()
            .AddScoped<IQueryDispatcher, QueryDispatcher>();
        #endregion

        return services; 
    }
}
