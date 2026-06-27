namespace Sigillum.Arcavis.Core.Application.Contracts.ROM.Users.Base;

public sealed record UserRom
{
    public Guid Id { get; init; }
    public bool IsActive { get; init; }
}
