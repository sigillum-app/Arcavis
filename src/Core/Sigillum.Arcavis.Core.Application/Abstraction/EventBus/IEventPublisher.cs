namespace Sigillum.Arcavis.Core.Application.Abstraction.EventBus;

public interface IEventPublisher
{
    Task PublishAsync(string eventType, string payload, CancellationToken cancellationToken = default);
}
