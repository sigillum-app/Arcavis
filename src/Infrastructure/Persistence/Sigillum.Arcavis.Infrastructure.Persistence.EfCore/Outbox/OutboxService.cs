using Microsoft.EntityFrameworkCore;
using Sigillum.Arcavis.Core.Application.Contracts.Outbox;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Context;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Outbox;

internal sealed class OutboxService : IOutboxService
{
    private readonly ArcavisContext _context;

    public OutboxService(ArcavisContext context)
    {
        _context = context;
    }

    public Task AddAsync(string type, string payload, DateTime occurredAt, CancellationToken ct = default)
    {
        var outboxMessage = new OutboxMessage(type, payload, occurredAt);

        _context.OutboxMessages.Add(outboxMessage);

        return Task.CompletedTask;
    }

    public async Task MarkAsProcessedAsync(Guid id, CancellationToken ct = default)
    {
        var message = await _context.OutboxMessages
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (message is null) return;

        message.ProcessedAt = DateTime.UtcNow;
        message.Error = null;
        message.NextRetryAt = null;
    }

    public async Task MarkAsFailedAsync(Guid id, string error, CancellationToken ct = default)
    {
        var message = await _context.OutboxMessages
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (message is null) return;

        message.Error = error;
        message.RetryCount++;

        if (message.RetryCount < message.MaxRetryCount)
        {
            var delay = Math.Pow(2, message.RetryCount);
            message.NextRetryAt = DateTime.UtcNow.AddMinutes(delay);
        }
        else
        {
            message.NextRetryAt = null;
        }
    }
}
