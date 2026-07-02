using Microsoft.EntityFrameworkCore;
using Sigillum.Arcavis.Core.Domain.Users;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Common.Conventions;
using Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Outbox;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Context;

public sealed class ArcavisContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public ArcavisContext(DbContextOptions<ArcavisContext> options) : base(options)
    { }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Add(_ => new AuditConvention());
        configurationBuilder.Conventions.Add(_ => new DomainEventConvention());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArcavisContext).Assembly);
    }
}
