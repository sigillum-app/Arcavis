using Sigillum.Arcavis.Core.Application.Abstraction.Outbox;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Context;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Outbox;

internal sealed class OutboxService : IOutboxService
{
    private readonly ArcavisContext _context;

    public OutboxService(ArcavisContext context)
    {
        _context = context;
    }

    public Task AddAsync(string type, string payload, DateTime occurredAt, CancellationToken cancellationToken = default)
    {
        var outboxMessage = new OutboxMessage(type, payload, occurredAt);
        _context.OutboxMessages.Add(outboxMessage);
        return Task.CompletedTask;
    }
}
