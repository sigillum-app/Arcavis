using Sigillum.Arcavis.Core.Application.Abstraction.ROM.Outbox.Base;

namespace Sigillum.Arcavis.Core.Application.Abstraction.Persistence.QueryServices;

public interface IOutboxQueryService
{
    Task<IReadOnlyList<OutboxRom>> GetUnprocessedMessagesAsync(int batchSize, CancellationToken cancellationToken = default);
}