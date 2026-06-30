using Sigillum.Arcavis.Core.Domain.Common;

namespace Sigillum.Arcavis.Core.Domain.Users.Passwords;

public static class PasswordError
{
    #region 1020 - 1029 => User Entity Errors
    public static readonly DomainError InvalidUserId =
        new(1020, "INVALID_USER_ID", nameof(Password));

    public static readonly DomainError InvalidPassword =
        new(1021, "INVALID_PASSWORD", nameof(Password));
    #endregion
}
