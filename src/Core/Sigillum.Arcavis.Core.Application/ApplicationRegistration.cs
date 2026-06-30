using FluentValidation;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Sigillum.Arcavis.Core.Application.Common.Behaviors;
using Sigillum.Arcavis.Core.Application.Contracts.Events;
using Sigillum.Arcavis.Core.Application.Features.Users.Commands.RegisterUser.Events.UserRegisteredIntegrationEvent;
using System.Reflection;

namespace Sigillum.Arcavis.Core.Application;

public static class ApplicationRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator((MediatorOptions options) =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(DomainEventToOutboxBehavior<,>));

        services.AddScoped<IIntegrationEventMapper, UserRegisteredIntegrationEventMapper>();

        return services;
    }
}
