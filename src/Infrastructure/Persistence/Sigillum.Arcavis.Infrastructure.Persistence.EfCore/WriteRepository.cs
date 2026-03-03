using Sigillum.Arcavis.Core.Application.Abstraction.EfCore;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Context;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore;

public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : class
{
    private readonly ArcavisContext _context;

    public WriteRepository(ArcavisContext context)
    {
        _context = context;
    }

    public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context.Set<TEntity>().Add(entity);

        return Task.CompletedTask;
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
    }
}
