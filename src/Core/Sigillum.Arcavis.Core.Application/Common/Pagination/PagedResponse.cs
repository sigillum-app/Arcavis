namespace Sigillum.Arcavis.Core.Application.Common.Pagination;

public record PagedResponse<T>(
    IEnumerable<T> Items,
    int TotalCount,
    int TotalPages,
    int CurrentPage,
    bool HasNextPage,
    bool HasPreviousPage
);
