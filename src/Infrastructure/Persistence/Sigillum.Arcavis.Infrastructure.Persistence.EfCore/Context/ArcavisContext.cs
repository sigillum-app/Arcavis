using Microsoft.EntityFrameworkCore;
using Sigillum.Arcavis.Core.Domain.Entities;

namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Context;

public sealed class ArcavisContext : DbContext
{
    public DbSet<UserEntity> Users{ get; set; }
    public DbSet<UserEmailEntity> UserEmails { get; set; }
    public DbSet<UserPasswordEntity> UserPasswords { get; set; }

    public ArcavisContext(DbContextOptions<ArcavisContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArcavisContext).Assembly);
    }
}
