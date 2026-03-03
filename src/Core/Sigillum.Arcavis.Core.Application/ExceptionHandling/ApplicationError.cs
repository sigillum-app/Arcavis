namespace Sigillum.Arcavis.Core.Application.ExceptionHandling;

public sealed class ApplicationError
{
    public int Code { get; }
    public string Message { get; }
    public string Source { get; }
    public string Layer { get; }
    public string? Detail { get; }

    internal ApplicationError(
        int code,
        string message,
        string source,
        string? detail = null)
    {
        Code = code;
        Message = message;
        Source = source;
        Layer = "2";
        Detail = detail;
    }

    public ApplicationError WithDetail(string detail)
        => new(Code, Message, Source, detail);
}