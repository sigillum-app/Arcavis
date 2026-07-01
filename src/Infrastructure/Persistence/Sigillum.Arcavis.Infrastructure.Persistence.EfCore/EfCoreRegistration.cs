using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sigillum.Arcavis.Core.Application.Contracts.Outbox;
using Sigillum.Arcavis.Core.Application.Contracts.Persistence;
using Sigillum.Arcavis.Core.Domain.Users;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Context;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Interceptors;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Outbox;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Repositories;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore;

public static class EfCoreRegistration
{
    public static IServiceCollection AddEfCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ArcavisContext>((sp, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgreSqlConnection"));

            options.AddInterceptors(sp.GetRequiredService<AuditInterceptor>());
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOutboxService, OutboxService>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
 