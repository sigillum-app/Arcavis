using Sigillum.Arcavis.Core.Application.CQRS;

namespace Sigillum.Arcavis.Core.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersQuery : IQuery<IReadOnlyList<GetAllUsersDto>>;
