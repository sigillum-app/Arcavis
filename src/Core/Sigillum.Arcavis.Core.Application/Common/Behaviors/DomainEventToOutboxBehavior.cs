using MediatR;
using Microsoft.Extensions.Logging;
using Sigillum.Arcavis.Core.Application.Abstraction.Events;
using Sigillum.Arcavis.Core.Application.Abstraction.Outbox;
using Sigillum.Arcavis.Core.Application.Abstraction.Persistence;
using System.Text.Json;

namespace Sigillum.Arcavis.Core.Application.Common.Behaviors;

public class DomainEventToOutboxBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
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

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();

        var domainEvents = _uow.GetDomainEvents().ToList();

        var requestName = typeof(TRequest).Name;


        foreach (var domainEvent in domainEvents)
        {
            _logger.LogInformation("--- Begin Outbox for {RequestName}", domainEvent);

            if (!_mapperDict.TryGetValue(domainEvent.GetType(), out var mapper))
                continue;

            var integrationEvent = mapper.Map(domainEvent);

            await _outbox.AddAsync(
                mapper.EventName,
                JsonSerializer.Serialize(integrationEvent),
                domainEvent.OccurredAt,
                cancellationToken);

            _logger.LogInformation("--- End Outbox for {RequestName}", domainEvent);
        }

        _uow.ClearDomainEvents();

        return response;
    }
}