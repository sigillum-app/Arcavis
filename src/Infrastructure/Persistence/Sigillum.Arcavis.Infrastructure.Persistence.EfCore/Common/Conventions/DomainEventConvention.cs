using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Sigillum.Arcavis.Core.Domain.Base;
using Sigillum.Arcavis.Core.Domain.Common;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Common.Conventions;

internal sealed class DomainEventConvention : IEntityTypeAddedConvention, IModelFinalizingConvention
{
    private const string DomainEventsField = "_domainEvents";

    public void ProcessEntityTypeAdded(
        IConventionEntityTypeBuilder entityTypeBuilder,
        IConventionContext<IConventionEntityTypeBuilder> context)
    {
        var clrType = entityTypeBuilder.Metadata.ClrType;
        if (clrType is null)
            return;

        if (typeof(DomainEvent).IsAssignableFrom(clrType))
        {
            entityTypeBuilder.ModelBuilder.HasNoEntityType(entityTypeBuilder.Metadata, fromDataAnnotation: false);
            return;
        }

        if (!typeof(Entity).IsAssignableFrom(clrType))
            return;

        entityTypeBuilder.Ignore(nameof(Entity.DomainEvents), fromDataAnnotation: false);
        entityTypeBuilder.Ignore(DomainEventsField, fromDataAnnotation: false);
    }

    public void ProcessModelFinalizing(IConventionModelBuilder modelBuilder, IConventionContext<IConventionModelBuilder> context)
    {
        foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
        {
            if (entityType.ClrType is null || !typeof(Entity).IsAssignableFrom(entityType.ClrType))
                continue;

            entityType.Builder.Ignore(nameof(Entity.DomainEvents), fromDataAnnotation: false);
            entityType.Builder.Ignore(DomainEventsField, fromDataAnnotation: false);
        }

        foreach (var entityType in modelBuilder.Metadata.GetEntityTypes().ToList())
        {
            if (entityType.ClrType is null || !typeof(DomainEvent).IsAssignableFrom(entityType.ClrType))
                continue;

            modelBuilder.HasNoEntityType(entityType, fromDataAnnotation: false);
        }
    }
}
