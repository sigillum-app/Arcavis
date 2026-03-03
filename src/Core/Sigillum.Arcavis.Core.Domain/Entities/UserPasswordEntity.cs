using Sigillum.Arcavis.Core.Domain.Entities.Base;
using Sigillum.Arcavis.Core.Domain.Errors;
using Sigillum.Arcavis.Core.Domain.ExceptionHandling;

namespace Sigillum.Arcavis.Core.Domain.Entities;

public sealed class UserPasswordEntity : BaseEntity
{
    public Guid UserId { get; private set; }
    public string PasswordHash { get; private set; }

    protected UserPasswordEntity() { }

    public UserPasswordEntity(
        Guid id,
        Guid userId,
        string passwordHash) : base(id)
    {
        if (userId == Guid.Empty)
            throw new DomainException(UserPasswordEntityError.InvalidUserId);

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainException(UserPasswordEntityError.InvalidPassword);

        UserId = userId;
        PasswordHash = passwordHash;
    }

    public void ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new DomainException(UserPasswordEntityError.InvalidPassword);

        PasswordHash = newPasswordHash;
    }
}
