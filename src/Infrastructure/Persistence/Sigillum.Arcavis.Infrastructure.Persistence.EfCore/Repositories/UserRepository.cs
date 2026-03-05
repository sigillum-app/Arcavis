using Sigillum.Arcavis.Core.Domain.Users;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Context;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ArcavisContext _context;

    public UserRepository(ArcavisContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FindAsync(new object[] { id }, cancellationToken);
    }
}
