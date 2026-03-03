using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sigillum.Arcavis.Core.Application.Abstraction.EfCore;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Context;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Extensions;

public static class EfCoreExtensions
{
    public static IServiceCollection AddEfCoreRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ArcavisContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgreSqlConnection"));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

        return services;
    }
}
 