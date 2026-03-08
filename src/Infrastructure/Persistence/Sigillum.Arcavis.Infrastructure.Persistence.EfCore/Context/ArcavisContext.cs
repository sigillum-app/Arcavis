using Microsoft.EntityFrameworkCore;
using Sigillum.Arcavis.Core.Domain.Base;
using Sigillum.Arcavis.Core.Domain.Users;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Outbox;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Context;

public sealed class ArcavisContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public ArcavisContext(DbContextOptions<ArcavisContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArcavisContext).Assembly);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
            .Where(e => typeof(Entity).IsAssignableFrom(e.ClrType) && !e.IsOwned()))
        {
            modelBuilder.Entity(entityType.ClrType).Ignore("DomainEvents");
            modelBuilder.Entity(entityType.ClrType).Property<Guid?>("CREATED_BY").HasColumnOrder(1);
            modelBuilder.Entity(entityType.ClrType).Property<DateTime>("CREATED_AT").HasColumnOrder(2);
            modelBuilder.Entity(entityType.ClrType).Property<Guid?>("UPDATED_BY").HasColumnOrder(3);
            modelBuilder.Entity(entityType.ClrType).Property<DateTime>("UPDATED_AT").HasColumnOrder(4);
            modelBuilder.Entity(entityType.ClrType).Property<bool>("IS_DELETED").HasDefaultValue(false).HasColumnOrder(5);
        }
    }
}
