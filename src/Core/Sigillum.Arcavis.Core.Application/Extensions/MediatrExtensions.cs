using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Sigillum.Arcavis.Core.Application.Common.Behaviors;

namespace Sigillum.Arcavis.Core.Application.Extensions;

public static class MediatrExtensions
{
    public static IServiceCollection AddMediatrRegistration(this IServiceCollection services)
    {
        services.AddMediator((MediatorOptions options) =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
        });

        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>))
                .AddSingleton(typeof(IPipelineBehavior<,>), typeof(DomainEventToOutboxBehavior<,>));

        return services;
    }
}
