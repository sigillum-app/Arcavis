using Sigillum.Arcavis.Core.Domain.Entities;
using Sigillum.Arcavis.Core.Domain.ExceptionHandling;

namespace Sigillum.Arcavis.Core.Domain.Errors;

public static class UserPasswordEntityError
{
    #region 1300 - 1399 => User Entity Errors
    public static readonly DomainError InvalidUserId =
        new(1300, "INVALID_USER_ID", nameof(UserEmailEntity));

    public static readonly DomainError InvalidPassword =
        new(1301, "INVALID_PASSWORD", nameof(UserEmailEntity));
    #endregion
}
