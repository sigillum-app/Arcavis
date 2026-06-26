using Sigillum.Arcavis.Core.Application.Common.CQRS;

namespace Sigillum.Arcavis.Core.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersQuery : IAppQuery<IReadOnlyList<GetAllUsersDto>>;
