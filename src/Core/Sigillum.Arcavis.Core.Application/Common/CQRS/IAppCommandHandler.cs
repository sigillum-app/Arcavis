using Mediator;

namespace Sigillum.Arcavis.Core.Application.Common.CQRS;

public interface IAppCommandHandler<in TRequest, TResponse>
    : IRequestHandler<TRequest, TResponse>
    where TRequest : IAppCommand<TResponse>
{ }

public interface IAppCommandHandler<in TRequest>
    : IRequestHandler<TRequest, Unit>
    where TRequest : IAppCommand
{ }