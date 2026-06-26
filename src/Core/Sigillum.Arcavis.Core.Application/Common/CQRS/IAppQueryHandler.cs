using Mediator;

namespace Sigillum.Arcavis.Core.Application.Common.CQRS;

public interface IAppQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, TResponse>
        where TQuery : IAppQuery<TResponse>
{ }