using Sigillum.Arcavis.Core.Domain.Entities;
using Sigillum.Arcavis.Core.Domain.Errors;
using Sigillum.Arcavis.Core.Domain.Exceptions;

namespace Sigillum.Arcavis.Core.Domain.Services;

public sealed class UserPasswordDomainService
{
    public UserPasswordEntity CreateUserPassword(Guid userId, string passwordHash)
    {
        var userPasswordId = Guid.NewGuid();

        var userPassword = UserPasswordEntity.Create(userPasswordId, userId, passwordHash);

        return userPassword;
    }

    public void ChangeUserPassword(UserPasswordEntity userPassword, string newPasswordHash)
    {
        if (userPassword == null)
            throw new DomainException(DomainError.InvalidUserPassword);

        if (newPasswordHash == null)
            throw new DomainException(DomainError.InvalidPassword);

        userPassword.ChangePassword(newPasswordHash);
    }
}
