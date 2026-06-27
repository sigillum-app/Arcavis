using Microsoft.Extensions.DependencyInjection;
using Sigillum.Arcavis.Core.Application.Abstraction.Events;
using Sigillum.Arcavis.Core.Application.Features.Users.Commands.RegisterUser;

namespace Sigillum.Arcavis.Core.Application.Extensions;

public static class ApplicationRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatrRegistration();

        services.AddScoped<IIntegrationEventMapper, UserRegisteredIntegrationEventMapper>();

        return services;
    }
}
