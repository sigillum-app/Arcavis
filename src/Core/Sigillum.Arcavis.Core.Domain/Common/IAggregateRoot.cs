namespace Sigillum.Arcavis.Core.Domain.Common;

public interface IAggregateRoot
{
    IReadOnlyList<DomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
