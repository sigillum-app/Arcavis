using RepoDb;
using Sigillum.Arcavis.Core.Application.Contracts.ROM.Users.Base;

namespace Sigillum.Arcavis.Infrastructure.Persistence.RepoDb.Mapping.Base;

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
