using Sigillum.Arcavis.Core.Domain.Entities;
using Sigillum.Arcavis.Core.Domain.Errors;
using Sigillum.Arcavis.Core.Domain.Exceptions;

namespace Sigillum.Arcavis.Core.Domain.Services;

public sealed class UserDomainService
{
    private readonly UserEmailDomainService _userEmailDomainService;
    private readonly UserPasswordDomainService _userPasswordDomainService;

    public UserDomainService(
        UserEmailDomainService userEmailDomainService,
        UserPasswordDomainService userPasswordDomainService)
    {
        _userEmailDomainService = userEmailDomainService;
        _userPasswordDomainService = userPasswordDomainService;
    }

    public UserEntity Register(
    string email,
    string passwordHash)
    {
        var userId = Guid.NewGuid();

        var user = UserEntity.Create(userId);

        _userPasswordDomainService.CreateUserPassword(userId, passwordHash);
        _userEmailDomainService.CreateUserEmail(userId, email);

        return (user);
    }

    public void DeactivateUser(UserEntity user)
    {
        if (user == null)
            throw new DomainException(DomainError.InvalidUser);

        user.Deactivate();
    }

    public void ActivateUser(UserEntity user)
    {
        if (user == null)
            throw new DomainException(DomainError.InvalidUser);

        user.Activate();
    }
}