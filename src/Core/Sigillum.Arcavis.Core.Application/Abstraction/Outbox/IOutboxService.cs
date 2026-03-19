namespace Sigillum.Arcavis.Core.Application.Abstraction.Outbox;

public interface IOutboxService
{
    Task AddAsync(string type, string payload, DateTime occurredAt, CancellationToken cancellationToken = default);
    Task MarkAsProcessedAsync(Guid id, CancellationToken ct = default);
    Task MarkAsFailedAsync(Guid id, string error, CancellationToken ct = default);
}