using Sigillum.Arcavis.Core.Application.Abstraction.Persistence.QueryServices;
using Sigillum.Arcavis.Core.Application.Common.CQRS;

namespace Sigillum.Arcavis.Core.Application.Features.Users.Queries.GetAllUsers;

internal sealed class GetAllUsersQueryHandler : IAppQueryHandler<GetAllUsersQuery, IReadOnlyList<GetAllUsersDto>>
{
    private readonly IUserQueryService _userQueryService;

    public GetAllUsersQueryHandler(IUserQueryService userQueryService)
    {
        _userQueryService = userQueryService;
    }

    public async ValueTask<IReadOnlyList<GetAllUsersDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userQueryService.GetAllAsync(cancellationToken);

        return users.Select(u => new GetAllUsersDto(
            Id: u.Id,
            IsActive: u.IsActive
        )).ToList();
    }
}
