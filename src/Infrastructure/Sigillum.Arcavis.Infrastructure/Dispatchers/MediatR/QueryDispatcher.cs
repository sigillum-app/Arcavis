using Mediator;
using Sigillum.Arcavis.Core.Application.Abstraction.Dispatcher;
using Sigillum.Arcavis.Core.Application.Common.CQRS;

namespace Sigillum.Arcavis.Infrastructure.Dispatchers.MediatR;

internal sealed class QueryDispatcher : IAppQueryDispatcher
{
    private readonly IMediator _mediator;

    public QueryDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async ValueTask<TResult> SendAsync<TResult>(IAppQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(query, cancellationToken);
    }
}