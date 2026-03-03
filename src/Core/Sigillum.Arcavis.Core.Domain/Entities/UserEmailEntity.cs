using Sigillum.Arcavis.Core.Domain.Entities.Base;
using Sigillum.Arcavis.Core.Domain.Errors;
using Sigillum.Arcavis.Core.Domain.ExceptionHandling;

namespace Sigillum.Arcavis.Core.Domain.Entities;

public sealed class UserEmailEntity : BaseEntity
{
    public Guid UserId { get; private set; }
    public string Email { get; private set; }
    public bool IsVerified { get; private set; }

    protected UserEmailEntity() { }

    public UserEmailEntity(
        Guid id,
        Guid userId,
        string email) : base(id)
    {
        if (userId == Guid.Empty)
            throw new DomainException(UserEmailEntityErrors.InvalidUserId);

        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException(UserEmailEntityErrors.InvalidEmail);

        UserId = userId;
        Email = email.Trim().ToLowerInvariant();
        IsVerified = false;
    }

    public void Verify()
    {
        IsVerified = true;
    }

    public void ChangeEmail(string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newEmail))
            throw new DomainException(UserEmailEntityErrors.InvalidEmail);

        Email = newEmail.Trim().ToLowerInvariant();
        IsVerified = false;
    }
}
