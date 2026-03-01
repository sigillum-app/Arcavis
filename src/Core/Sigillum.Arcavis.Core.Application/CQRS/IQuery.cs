using MediatR;

namespace Sigillum.Arcavis.Core.Application.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse> { }
