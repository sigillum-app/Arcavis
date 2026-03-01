using Sigillum.Arcavis.Core.Domain.Entities;
using Sigillum.Arcavis.Core.Domain.Errors;
using Sigillum.Arcavis.Core.Domain.Exceptions;

namespace Sigillum.Arcavis.Core.Domain.Services;

public sealed class UserEmailDomainService
{
    public UserEmailEntity CreateUserEmail(Guid userId, string email)
    {
        var userEmailId = Guid.NewGuid();

        var userEmail = UserEmailEntity.Create(userEmailId, userId, email);

        return userEmail;
    }

    public void ChangeUserEmail(UserEmailEntity userEmail, string newEmail)
    {
        if (userEmail == null)
            throw new DomainException(DomainError.InvalidUserEmail);

        userEmail.ChangeEmail(newEmail);
    }

    public void VerifyUserEmail(UserEmailEntity userEmail)
    {
        if (userEmail == null)
            throw new DomainException(DomainError.InvalidEmail);

        userEmail.Verify();
    }
}
