using Sigillum.Arcavis.Core.Application.Common.CQRS;

namespace Sigillum.Arcavis.Core.Application.Abstraction.Dispatcher;

public interface IAppQueryDispatcher
{
    ValueTask<TResult> SendAsync<TResult>(IAppQuery<TResult> query, CancellationToken cancellationToken = default);
}