using MediatR;
using Sigillum.Arcavis.Core.Application.Contracts.Dispatcher;
using Sigillum.Arcavis.Core.Application.CQRS;

namespace Sigillum.Arcavis.Infrastructure.Dispatchers.MediatR;

internal sealed class CommandDispatcher : ICommandDispatcher
{
    private readonly IMediator _mediator;

    public CommandDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task SendAsync(ICommand command, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(command, cancellationToken);
    }

    public Task<TResult> SendAsync<TResult>(
        ICommand<TResult> command,
        CancellationToken cancellationToken = default)
    {
        return _mediator.Send(command, cancellationToken);
    }
}