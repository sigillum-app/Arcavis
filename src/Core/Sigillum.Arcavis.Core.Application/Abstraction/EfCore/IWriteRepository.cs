namespace Sigillum.Arcavis.Core.Application.Abstraction.EfCore;

public interface IWriteRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
