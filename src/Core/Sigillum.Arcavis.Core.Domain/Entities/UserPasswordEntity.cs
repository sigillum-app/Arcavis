using Sigillum.Arcavis.Core.Domain.Entities.Base;
using Sigillum.Arcavis.Core.Domain.Errors;
using Sigillum.Arcavis.Core.Domain.Exceptions;

namespace Sigillum.Arcavis.Core.Domain.Entities;

public sealed class UserPasswordEntity : BaseEntity
{
    public Guid UserId { get; private set; }
    public string PasswordHash { get; private set; }

    protected UserPasswordEntity() { }

    private UserPasswordEntity(
        Guid id,
        Guid userId,
        string passwordHash) : base(id)
    {
        UserId = userId;
        PasswordHash = passwordHash;
    }

    public static UserPasswordEntity Create(Guid id, Guid userId, string passwordHash)
    {
        if (id == Guid.Empty)
            throw new DomainException(DomainError.InvalidUserPasswordId);

        if (userId == Guid.Empty)
            throw new DomainException(DomainError.InvalidUserId);

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainException(DomainError.InvalidUserPassword);

        return new UserPasswordEntity(id, userId, passwordHash);
    }

    public void ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new DomainException(DomainError.InvalidUserPassword);

        PasswordHash = newPasswordHash;
    }
}
