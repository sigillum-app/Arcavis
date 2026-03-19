namespace Sigillum.Arcavis.Core.Application.Abstraction.Events;

public interface IIntegrationEventMapper
{
    Type DomainEventType { get; }
    Type IntegrationEventType { get; }
    string EventName { get; }
    object Map(object domainEvent);
}

public interface IIntegrationEventMapper<TDomainEvent> : IIntegrationEventMapper
{
    object Map(TDomainEvent domainEvent);
}