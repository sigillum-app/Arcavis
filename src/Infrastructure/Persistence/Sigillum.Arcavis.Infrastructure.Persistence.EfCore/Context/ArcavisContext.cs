using Microsoft.EntityFrameworkCore;
using Sigillum.Arcavis.Core.Domain.Users;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Context;

public sealed class ArcavisContext : DbContext
{
    public DbSet<User> Users{ get; set; }

    public ArcavisContext(DbContextOptions<ArcavisContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArcavisContext).Assembly);
    }
}
