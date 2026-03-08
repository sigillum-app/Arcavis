using Sigillum.Arcavis.Core.Application.Abstraction.ROM.Users.Base;
using Sigillum.Arcavis.Core.Application.Common.Pagination;

namespace Sigillum.Arcavis.Core.Application.Abstraction.Persistence.QueryServices;

public interface IUserQueryService
{
    Task<UserRom?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResponse<UserRom>> GetPagedAsync(PaginationParams pagination, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<UserRom>> GetAllAsync(CancellationToken cancellationToken = default);
}