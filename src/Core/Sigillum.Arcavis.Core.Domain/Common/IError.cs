namespace Sigillum.Arcavis.Core.Domain.Common;

public interface IError
{
    int Code { get; }
    string Key { get; }
    string Source { get; }
    string Layer { get; }
    string? Detail { get; }

}
