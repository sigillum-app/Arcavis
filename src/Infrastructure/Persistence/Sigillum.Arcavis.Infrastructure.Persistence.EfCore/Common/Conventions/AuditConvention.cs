using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Sigillum.Arcavis.Core.Domain.Base;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Common.Conventions;

internal sealed class AuditConvention : IModelFinalizingConvention
{
    public void ProcessModelFinalizing(
        IConventionModelBuilder modelBuilder,
        IConventionContext<IConventionModelBuilder> context)
    {
        foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
        {
            var clrType = entityType.ClrType;
            if (clrType is null)
                continue;

            if (!entityType.IsOwned() &&
                !typeof(Entity).IsAssignableFrom(clrType))
                continue;

            var builder = entityType.Builder;

            builder.Property(typeof(DateTime), AuditProperties.CreatedAt);
            builder.Property(typeof(Guid?), AuditProperties.CreatedBy);
            builder.Property(typeof(DateTime), AuditProperties.UpdatedAt);
            builder.Property(typeof(Guid?), AuditProperties.UpdatedBy);
        }
    }
}
