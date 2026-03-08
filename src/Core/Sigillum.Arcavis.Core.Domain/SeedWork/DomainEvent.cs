namespace Sigillum.Arcavis.Core.Domain.SeedWork;

public abstract record DomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime OccuredAt { get; } = DateTime.UtcNow;
}
