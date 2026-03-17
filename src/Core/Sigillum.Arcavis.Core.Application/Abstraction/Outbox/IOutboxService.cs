namespace Sigillum.Arcavis.Core.Application.Abstraction.Outbox;

public interface IOutboxService
{
    Task AddAsync(string type, string payload, DateTime occurredAt, CancellationToken cancellationToken = default);
}