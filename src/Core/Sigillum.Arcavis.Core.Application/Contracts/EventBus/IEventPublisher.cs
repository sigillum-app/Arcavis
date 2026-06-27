namespace Sigillum.Arcavis.Core.Application.Contracts.EventBus;

public interface IEventPublisher
{
    Task PublishAsync(string eventType, string payload, CancellationToken cancellationToken = default);
}
