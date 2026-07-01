using Sigillum.Arcavis.Core.Domain.Base;
using Sigillum.Arcavis.Core.Domain.Common;

namespace Sigillum.Arcavis.Core.Domain.Users.Passwords;

public sealed class Password
{
    public PasswordId Id { get; private set; }
    public string PasswordHash { get; private set; }
    public DateTime StartAt { get; private set; }

    protected Password() { }

    private Password(string passwordHash)
    {
        Id = new PasswordId(Guid.NewGuid());
        PasswordHash = passwordHash;
        StartAt = DateTime.UtcNow;
    }

    internal static Result<Password> Create(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            return Result<Password>.Failure(PasswordError.InvalidPassword);

        return Result<Password>.Success(new Password(passwordHash));
    }
}
