using Sigillum.Arcavis.Core.Application.CQRS;

namespace Sigillum.Arcavis.Core.Application.Abstraction.Dispatcher;

public interface IQueryDispatcher
{
    Task<TResult> SendAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}