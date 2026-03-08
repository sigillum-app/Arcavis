using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Sigillum.Arcavis.Core.Application.Abstraction.Persistence;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Context;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ArcavisContext _context;
    private IDbContextTransaction? _currentTransaction;
    private IDomainEventsDispatcher _domainEventsDispatcher;

    public UnitOfWork(
        ArcavisContext context, 
        IDomainEventsDispatcher domainEventsDispatcher)
    {
        _context = context;
        _domainEventsDispatcher = domainEventsDispatcher;
    }

    public bool HasActiveTransaction => _currentTransaction is not null;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);

    public async Task<T> ExecuteInTransactionAsync<T>(Func<CancellationToken, Task<T>> action, CancellationToken cancellationToken = default)
    {
        var strategy = _context.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var result = await action(cancellationToken);

                await _domainEventsDispatcher.DispatchAsync(cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return result;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        });
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction is not null)
            return;

        _currentTransaction =
            await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction is null)
            return;

        try
        {
            await _domainEventsDispatcher.DispatchAsync(cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            await _currentTransaction.CommitAsync(cancellationToken);
        }
        finally
        {
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction is null)
            return;

        try
        {
            await _currentTransaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }
}