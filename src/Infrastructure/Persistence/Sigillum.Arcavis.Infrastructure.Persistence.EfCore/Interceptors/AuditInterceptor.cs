using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Configurations.Base;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Interceptors;

internal sealed class AuditInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        UpdateAudit(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateAudit(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void UpdateAudit(DbContext? context)
    {
        if (context is null)
            return;

        var now = DateTime.UtcNow;

        // TODO: Get the current user ID from the context or a service
        Guid? currentUserId = null;

        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.State is not (EntityState.Added or EntityState.Modified))
                continue;

            if (!entry.Metadata.FindProperty(AuditProperties.CreatedAt)?.IsShadowProperty() ?? true)
                continue;

            if (entry.State == EntityState.Added)
            {
                entry.Property(AuditProperties.CreatedAt).CurrentValue = now;
                entry.Property(AuditProperties.CreatedBy).CurrentValue = currentUserId;
            }

            entry.Property(AuditProperties.UpdatedAt).CurrentValue = now;
            entry.Property(AuditProperties.UpdatedBy).CurrentValue = currentUserId;
        }
    }
}
