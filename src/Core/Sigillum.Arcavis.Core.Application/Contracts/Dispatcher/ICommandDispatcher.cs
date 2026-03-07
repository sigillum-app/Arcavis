using Sigillum.Arcavis.Core.Application.CQRS;

namespace Sigillum.Arcavis.Core.Application.Contracts.Dispatcher;

public interface ICommandDispatcher
{
    Task SendAsync(ICommand command, CancellationToken cancellationToken = default);

    Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);
}