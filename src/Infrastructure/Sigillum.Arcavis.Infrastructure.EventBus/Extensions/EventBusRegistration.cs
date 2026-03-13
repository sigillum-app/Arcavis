using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sigillum.Arcavis.Infrastructure.EventBus.Extensions;

public static class MessagingExtensions
{
    public static IServiceCollection AddEventBusRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        var host = configuration["RabbitMQ:Host"]!;
        var username = configuration["RabbitMQ:Username"]!;
        var password = configuration["RabbitMQ:Password"]!;

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(host, h =>
                {
                    h.Username(username);
                    h.Password(password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
