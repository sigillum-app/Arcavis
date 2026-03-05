using Sigillum.Arcavis.Core.Domain.Base;
using Sigillum.Arcavis.Core.Domain.SeedWork.ExceptionHandling;

namespace Sigillum.Arcavis.Core.Domain.Users.Passwords;

public sealed class Password : Entity
{
    public PasswordId Id { get; private set; }
    public string PasswordHash { get; private set; }

    protected Password () { }

    public Password (string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainException(PasswordError.InvalidPassword);

        Id = new PasswordId(Guid.NewGuid());
        PasswordHash = passwordHash;
    }

    public void ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new DomainException(PasswordError.InvalidPassword);

        PasswordHash = newPasswordHash;
    }
}
