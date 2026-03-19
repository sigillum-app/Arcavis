namespace Sigillum.Arcavis.Core.Domain.SeedWork;

public interface IAggregateRoot
{
    IReadOnlyList<DomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
