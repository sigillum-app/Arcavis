using Microsoft.Extensions.DependencyInjection;
using Sigillum.Arcavis.Core.Application.Common.Behaviors;
using System.Reflection;

namespace Sigillum.Arcavis.Core.Application.Extensions;

public static class MediatrExtensions
{
    public static IServiceCollection AddMediatrRegistration(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);

                cfg.AddBehavior(typeof(TransactionBehavior<,>));
            }
        );

        return services;
    }
}
