using MassTransit;
using Microsoft.Extensions.Logging;
using Sigillum.Arcavis.Core.Application.Contracts.EventBus;
using Sigillum.Arcavis.Core.Application.Contracts.Events;
using System.Text.Json;

namespace Sigillum.Arcavis.Infrastructure.EventBus.Publishers;

internal class EventPublisher : IEventPublisher
{
    #region Dependencies
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<EventPublisher> _logger;
    private readonly Dictionary<string, Type> _typeMap;

    public EventPublisher(
        IPublishEndpoint publishEndpoint,
        IEnumerable<IIntegrationEventMapper> mappers,
        ILogger<EventPublisher> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
        _typeMap = mappers.ToDictionary(
            m => m.EventName,
            m => m.IntegrationEventType);
    }

    #endregion

    public async Task PublishAsync(string eventType, string payload, CancellationToken cancellationToken = default)
    {
        if (!_typeMap.TryGetValue(eventType, out var type))
        {
            _logger.LogWarning("No integration event type found for {EventType}", eventType);
            return;
        }

        var integrationEvent = JsonSerializer.Deserialize(payload, type);

        if (integrationEvent is null)
        {
            _logger.LogWarning("Failed to deserialize payload for {EventType}", eventType);
            return;
        }

        await _publishEndpoint.Publish(integrationEvent, type, cancellationToken);
    }

}
