using MediatR;
using Sigillum.Arcavis.Core.Application.Abstraction.Dispatcher;
using Sigillum.Arcavis.Core.Application.CQRS;

namespace Sigillum.Arcavis.Infrastructure.Dispatchers.MediatR;

internal sealed class QueryDispatcher : IQueryDispatcher
{
    private readonly IMediator _mediator;

    public QueryDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<TResult> SendAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(query, cancellationToken);
    }
}