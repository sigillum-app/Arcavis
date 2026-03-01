using Sigillum.Arcavis.Core.Domain.Entities.Base;
using Sigillum.Arcavis.Core.Domain.Errors;
using Sigillum.Arcavis.Core.Domain.Exceptions;

namespace Sigillum.Arcavis.Core.Domain.Entities;

public sealed class UserEmailEntity : BaseEntity
{
    public Guid UserId { get; private set; }
    public string Email { get; private set; }
    public bool IsVerified { get; private set; }

    protected UserEmailEntity() { }

    private UserEmailEntity(
        Guid id,
        Guid userId,
        string email,
        bool isVerified) : base(id)
    {
        UserId = userId;
        Email = email.Trim().ToLowerInvariant();
        IsVerified = isVerified;
    }

    public static UserEmailEntity Create(Guid id, Guid userId, string email)
    {
        if (id == Guid.Empty)
            throw new DomainException(DomainError.InvalidUserEmailId);

        if (userId == Guid.Empty)
            throw new DomainException(DomainError.InvalidUserId);

        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException(DomainError.InvalidEmail);

        return new UserEmailEntity(id, userId, email, false);
    }

    public void Verify()
    {
        IsVerified = true;
    }

    public void ChangeEmail(string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newEmail))
            throw new DomainException(DomainError.InvalidEmail);

        Email = newEmail.Trim().ToLowerInvariant();
        IsVerified = false;
    }
}
