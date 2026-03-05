namespace Sigillum.Arcavis.Core.Application.Common.ExceptionHandling;

public sealed class ApplicationException : Exception
{
    public ApplicationError Error { get; }

    public ApplicationException(ApplicationError error) : base(error.Message)
    {
        Error = error;
    }
}