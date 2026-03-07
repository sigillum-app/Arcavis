using Sigillum.Arcavis.Core.Application.ROMs.Users.Base;
using RepoDb;

namespace Sigillum.Arcavis.Infrastructure.Persistence.RepoDb.Mapping;

internal static class UserRomMap
{
    internal static void Configure()
    {
        FluentMapper
            .Entity<UserRom>()
            .Table("USER")
            .Column(x => x.Id, "ID")
            .Column(x => x.IsActive, "IS_ACTIVE");
    }
}