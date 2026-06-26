using Sigillum.Arcavis.Core.Application.Common.CQRS;

namespace Sigillum.Arcavis.Core.Application.Abstraction.Dispatcher;

public interface IAppCommandDispatcher
{
    ValueTask SendAsync(IAppCommand command, CancellationToken cancellationToken = default);

    ValueTask<TResult> SendAsync<TResult>(IAppCommand<TResult> command, CancellationToken cancellationToken = default);
}