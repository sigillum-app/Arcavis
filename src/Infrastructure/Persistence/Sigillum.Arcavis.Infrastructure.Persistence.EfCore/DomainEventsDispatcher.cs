using Sigillum.Arcavis.Core.Application.Abstraction.Persistence;
using Sigillum.Arcavis.Core.Domain.Base;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Context;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Outbox;
using System.Text.Json;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore;

public sealed class DomainEventsDispatcher : IDomainEventsDispatcher
{
    #region Dependencies
    private readonly ArcavisContext _context;

    public DomainEventsDispatcher(ArcavisContext context)
    {
        _context = context;
    }
    #endregion

    public async Task DispatchAsync(CancellationToken cancellationToken = default)
    {
        var entities = _context.ChangeTracker
                               .Entries<Entity>()
                               .Where(e => e.Entity.DomainEvents.Any())
                               .Select(e => e.Entity)
                               .ToList();

        foreach (var entity in entities) 
        { 
            foreach (var domainEvent in entity.DomainEvents)
            {
                var outboxMessage = new OutboxMessage(
                domainEvent.GetType().FullName!,
                JsonSerializer.Serialize(domainEvent, domainEvent.GetType()),
                domainEvent.OccurredAt);

                _context.OutboxMessages.Add(outboxMessage);
            }
            entity.ClearDomainEvents();
        }
    }
}
