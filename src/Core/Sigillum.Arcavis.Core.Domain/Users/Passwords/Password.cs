using Sigillum.Arcavis.Core.Domain.Base;
using Sigillum.Arcavis.Core.Domain.Common;

namespace Sigillum.Arcavis.Core.Domain.Users.Passwords;

public sealed class Password : Entity
{
    public PasswordId Id { get; private set; }
    public string PasswordHash { get; private set; }

    protected Password() { }

    private Password(string passwordHash)
    {
        Id = new PasswordId(Guid.NewGuid());
        PasswordHash = passwordHash;
    }

    public static Result<Password> Create(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            return Result<Password>.Failure(PasswordError.InvalidPassword);

        return Result<Password>.Success(new Password(passwordHash));
    }

    public Result ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            return Result.Failure(PasswordError.InvalidPassword);

        PasswordHash = newPasswordHash;
        return Result.Success();
    }
}
