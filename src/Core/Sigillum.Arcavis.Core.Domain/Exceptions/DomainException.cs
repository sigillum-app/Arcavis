using Sigillum.Arcavis.Core.Domain.Errors;

namespace Sigillum.Arcavis.Core.Domain.Exceptions;


public sealed class DomainException : Exception
{
    public DomainError Error { get; }

    public DomainException(DomainError error)
        : base(error.Code)
    {
        Error = error;
    }
}