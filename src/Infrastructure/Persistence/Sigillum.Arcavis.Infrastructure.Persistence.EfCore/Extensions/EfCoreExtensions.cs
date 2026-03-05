using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sigillum.Arcavis.Core.Application.Abstraction;
using Sigillum.Arcavis.Core.Domain.Users;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Context;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Repositories;

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
        services.AddScoped<IUserRepository, UserRepository>();


        return services;
    }
}
 