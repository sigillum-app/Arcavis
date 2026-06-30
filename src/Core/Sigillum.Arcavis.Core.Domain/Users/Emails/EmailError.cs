using Sigillum.Arcavis.Core.Domain.Common;

namespace Sigillum.Arcavis.Core.Domain.Users.Emails;

public static class EmailError
{
    #region 1010 - 1019 => User Email Entity Errors
    public static readonly DomainError InvalidUserId =
        new(1010, "INVALID_USER_ID", nameof(Email));

    public static readonly DomainError InvalidEmail =
        new(1011, "INVALID_EMAIL", nameof(Email));

    public static readonly DomainError AlreadyVerified =
        new(1012, "ALREADY_VERIFIED", nameof(Email));
    #endregion
}
