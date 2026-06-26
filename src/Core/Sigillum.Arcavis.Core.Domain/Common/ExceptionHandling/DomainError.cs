namespace Sigillum.Arcavis.Core.Domain.Common.ExceptionHandling;

public sealed class DomainError
{
    public int Code { get; }
    public string Message { get; }
    public string Source { get; }
    public string Layer { get; }
    public string? Detail { get; }

    internal DomainError(
        int code,
        string message,
        string source,
        string? detail = null)
    {
        Code = code;
        Message = message;
        Source = source;
        Layer = "1";
        Detail = detail;
    }

    public DomainError WithDetail(string detail)
        => new(Code, Message, Source, detail);
}