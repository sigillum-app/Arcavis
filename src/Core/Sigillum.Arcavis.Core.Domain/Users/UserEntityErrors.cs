using Sigillum.Arcavis.Core.Domain.SeedWork.ExceptionHandling;

namespace Sigillum.Arcavis.Core.Domain.Users;

public static class UserErrors
{
    #region 1100 - 1199 => User Entity Errors
    public static readonly DomainError InvalidUserId =
        new(1100, "INVALID_USER_ID", nameof(User));
    #endregion
}
