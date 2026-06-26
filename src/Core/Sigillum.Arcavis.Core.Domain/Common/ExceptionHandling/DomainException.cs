namespace Sigillum.Arcavis.Core.Domain.Common.ExceptionHandling;

public sealed class DomainException : Exception
{
    public DomainError Error { get; }

    public DomainException(DomainError error) : base(error.Message)
    {
        Error = error;
    }
}