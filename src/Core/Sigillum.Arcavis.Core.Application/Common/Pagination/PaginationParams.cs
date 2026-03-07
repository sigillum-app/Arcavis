namespace Sigillum.Arcavis.Core.Application.Common.Pagination;

public record PaginationParams
{
    private const int MinPage = 1;
    private const int MinPageSize = 1;
    private const int MaxPageSize = 100;

    public int Page { get; init; }
    public int PageSize { get; init; }
    public int Offset => (Page - 1) * PageSize;

    public PaginationParams(int page, int pageSize)
    {
        Page = page < MinPage ? MinPage : page;
        PageSize = pageSize < MinPageSize ? MinPageSize : pageSize > MaxPageSize ? MaxPageSize : pageSize;
    }
}
