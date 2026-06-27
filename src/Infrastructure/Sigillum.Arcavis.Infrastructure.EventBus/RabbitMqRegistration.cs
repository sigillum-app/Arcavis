using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sigillum.Arcavis.Core.Application.Contracts.EventBus;
using Sigillum.Arcavis.Infrastructure.EventBus.Publishers;

namespace Sigillum.Arcavis.Infrastructure.EventBus;

public static class RabbitMqRegistration
{
    public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
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

        services.AddScoped<IEventPublisher, EventPublisher>();

        return services;
    }
}
