using Sigillum.Arcavis.Core.Domain.Common.ExceptionHandling;

namespace Sigillum.Arcavis.Core.Domain.Users.Emails;

public static class EmailErrors
{
    #region 1200 - 1299 => User Email Entity Errors
    public static readonly DomainError InvalidUserId =
        new(1200, "INVALID_USER_ID", nameof(Email));

    public static readonly DomainError InvalidEmail =
        new(1201, "INVALID_EMAIL", nameof(Email));
    #endregion
}
