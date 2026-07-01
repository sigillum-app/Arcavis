using Mediator;
using Microsoft.Extensions.Logging;
using Sigillum.Arcavis.Core.Application.Contracts.Events;
using Sigillum.Arcavis.Core.Application.Contracts.Outbox;
using Sigillum.Arcavis.Core.Application.Contracts.Persistence;
using System.Diagnostics;
using System.Text.Json;

namespace Sigillum.Arcavis.Core.Application.Common.Behaviors;

public class DomainEventToOutboxBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IMessage
{
    #region Dependencies
    private readonly IOutboxService _outbox;
    private readonly IUnitOfWork _uow;
    private readonly Dictionary<Type, IIntegrationEventMapper> _mapperDict;
    private readonly ILogger<DomainEventToOutboxBehavior<TRequest, TResponse>> _logger;

    public DomainEventToOutboxBehavior(
        IOutboxService outbox,
        IEnumerable<IIntegrationEventMapper> mappers,
        IUnitOfWork uow,
        ILogger<DomainEventToOutboxBehavior<TRequest, TResponse>> logger)
    {
        _outbox = outbox;
        _uow = uow;
        _mapperDict = mappers.GroupBy(x => x.DomainEventType).ToDictionary(g => g.Key, g => g.First());
        _logger = logger;
    }
    #endregion

    public async ValueTask<TResponse> Handle(TRequest message,MessageHandlerDelegate<TRequest, TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next(message, cancellationToken);

        var domainEvents = _uow.GetDomainEvents().ToList();

        foreach (var domainEvent in domainEvents)
        {
            _logger.LogInformation("--- Begin Outbox for {EventName}", domainEvent.GetType().Name);

            if (!_mapperDict.TryGetValue(domainEvent.GetType(), out var mapper))
                continue;

            if (mapper.Map(domainEvent) is not IntegrationEvent integrationEvent)
                continue;

            integrationEvent = integrationEvent with
            {
                SourceEventId = domainEvent.EventId,
                OccurredAt = domainEvent.OccurredAt,
            };

            await _outbox.AddAsync(
                mapper.EventName,
                JsonSerializer.Serialize(integrationEvent),
                domainEvent.OccurredAt,
                cancellationToken);

            _logger.LogInformation("--- End Outbox for {EventName}", domainEvent.GetType().Name);
        }

        _uow.ClearDomainEvents();

        return response;
    }
}
