namespace Sigillum.Arcavis.Core.Domain.Common;

public sealed class DomainError : IError
{
    public int Code { get; }
    public string Key { get; }
    public string Source { get; }
    public string Layer { get; }
    public string? Detail { get; }

    internal DomainError(
        int code,
        string key,
        string source,
        string? detail = null)
    {
        Code = code;
        Key = key;
        Source = source;
        Layer = "1";
        Detail = detail;
    }

    public DomainError WithDetail(string detail)
        => new(Code, Key, Source, detail);
}
