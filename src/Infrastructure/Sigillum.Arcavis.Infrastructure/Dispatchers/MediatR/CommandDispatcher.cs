using Mediator;
using Sigillum.Arcavis.Core.Application.Abstraction.Dispatcher;
using Sigillum.Arcavis.Core.Application.Common.CQRS;

namespace Sigillum.Arcavis.Infrastructure.Dispatchers.MediatR;

internal sealed class CommandDispatcher : IAppCommandDispatcher
{
    private readonly IMediator _mediator;

    public CommandDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async ValueTask SendAsync(IAppCommand command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
    }

    public ValueTask<TResult> SendAsync<TResult>(IAppCommand<TResult> command, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(command, cancellationToken);
    }
}