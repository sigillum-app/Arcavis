using Sigillum.Arcavis.Core.Domain.Common;

namespace Sigillum.Arcavis.Core.Application.Common;

public class ApplicationError : IError
{
    public int Code { get; }
    public string Key { get; }
    public string Source { get; }
    public string Layer { get; }
    public string? Detail { get; }

    internal ApplicationError(
        int code,
        string key,
        string source,
        string? detail = null)
    {
        Code = code;
        Key = key;
        Source = source;
        Layer = "2";
        Detail = detail;
    }

    public ApplicationError WithDetail(string detail)
        => new(Code, Key, Source, detail);
}
