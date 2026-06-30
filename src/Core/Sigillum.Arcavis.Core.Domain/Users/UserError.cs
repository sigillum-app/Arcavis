using Sigillum.Arcavis.Core.Domain.Common;

namespace Sigillum.Arcavis.Core.Domain.Users;

public static class UserError
{
    #region 1000 - 1009 => User Entity Errors
    public static readonly DomainError InvalidUserId =
        new(1000, "INVALID_USER_ID", nameof(User));

    public static readonly DomainError UserAlreadyActive = 
        new(1001, "USER_ALREADY_ACTIVE", nameof(User));

    public static readonly DomainError UserAlreadyInactive =
        new(1002, "USER_ALREADY_INACTIVE", nameof(User));
    #endregion
}
