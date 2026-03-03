using MediatR;

namespace Sigillum.Arcavis.Core.Application.CQRS;

public interface ICommand<out TResponse> 
    : IRequest<TResponse> 
{ }

public interface ICommand 
    : IRequest 
{ }