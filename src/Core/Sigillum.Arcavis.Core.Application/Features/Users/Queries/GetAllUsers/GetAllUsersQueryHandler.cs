using Mediator;
using Sigillum.Arcavis.Core.Application.Contracts.Persistence.QueryServices;

namespace Sigillum.Arcavis.Core.Application.Features.Users.Queries.GetAllUsers;

internal sealed class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, IReadOnlyList<GetAllUsersDto>>
{
    private readonly IUserQueryService _userQueryService;

    public GetAllUsersQueryHandler(IUserQueryService userQueryService)
    {
        _userQueryService = userQueryService;
    }

    public async ValueTask<IReadOnlyList<GetAllUsersDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userQueryService.GetAllAsync(cancellationToken);

        return users
            .Select(u => new GetAllUsersDto(
                u.Id,
                u.IsActive))
            .ToArray();
    }
}
