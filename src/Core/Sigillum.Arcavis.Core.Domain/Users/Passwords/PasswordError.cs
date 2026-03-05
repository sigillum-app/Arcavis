using Sigillum.Arcavis.Core.Domain.SeedWork.ExceptionHandling;

namespace Sigillum.Arcavis.Core.Domain.Users.Passwords;

public static class PasswordError
{
    #region 1300 - 1399 => User Entity Errors
    public static readonly DomainError InvalidUserId =
        new(1300, "INVALID_USER_ID", nameof(Password));

    public static readonly DomainError InvalidPassword =
        new(1301, "INVALID_PASSWORD", nameof(Password));
    #endregion
}
