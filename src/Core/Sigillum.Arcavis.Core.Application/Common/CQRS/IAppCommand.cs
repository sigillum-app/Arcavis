using Mediator;

namespace Sigillum.Arcavis.Core.Application.Common.CQRS;

public interface IAppCommand<out TResponse> 
    : IRequest<TResponse> 
{ }

public interface IAppCommand 
    : IRequest 
{ }