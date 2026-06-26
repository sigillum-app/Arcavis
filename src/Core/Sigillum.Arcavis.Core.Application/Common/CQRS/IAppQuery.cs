using Mediator;

namespace Sigillum.Arcavis.Core.Application.Common.CQRS;

public interface IAppQuery<out TResponse> 
    : IRequest<TResponse> 
{ }
