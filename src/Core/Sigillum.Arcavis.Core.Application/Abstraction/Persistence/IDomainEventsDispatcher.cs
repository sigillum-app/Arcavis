namespace Sigillum.Arcavis.Core.Application.Abstraction.Persistence;

public interface IDomainEventsDispatcher
{
    Task DispatchAsync(CancellationToken cancellationToken = default); 
}
