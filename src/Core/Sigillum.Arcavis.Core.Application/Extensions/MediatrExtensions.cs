using Microsoft.Extensions.DependencyInjection;
using Sigillum.Arcavis.Core.Application.Common.Behaviors;
using System.Reflection;

namespace Sigillum.Arcavis.Core.Application.Extensions;

public static class MediatrExtensions
{
    public static IServiceCollection AddMediatrRegistration(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
                cfg.AddOpenBehavior(typeof(DomainEventToOutboxBehavior<,>));
            }
        );

        return services;
    }
}
