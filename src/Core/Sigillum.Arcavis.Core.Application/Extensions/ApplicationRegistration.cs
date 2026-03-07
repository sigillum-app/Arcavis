using Microsoft.Extensions.DependencyInjection;

namespace Sigillum.Arcavis.Core.Application.Extensions;

public static class ApplicationRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatrRegistration();

        return services;
    }
}
