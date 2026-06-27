using Sigillum.Arcavis.Core.Application.Contracts.ROM.Outbox.Base;

namespace Sigillum.Arcavis.Core.Application.Contracts.Persistence.QueryServices;

public interface IOutboxQueryService
{
    Task<IReadOnlyList<OutboxRom>> GetUnprocessedMessagesAsync(int batchSize, CancellationToken cancellationToken = default);
}