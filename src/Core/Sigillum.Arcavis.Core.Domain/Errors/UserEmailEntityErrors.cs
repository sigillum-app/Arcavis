using Sigillum.Arcavis.Core.Domain.Entities;
using Sigillum.Arcavis.Core.Domain.ExceptionHandling;

namespace Sigillum.Arcavis.Core.Domain.Errors;

public static class UserEmailEntityErrors
{
    #region 1200 - 1299 => User Email Entity Errors
    public static readonly DomainError InvalidUserId =
        new(1200, "INVALID_USER_ID", nameof(UserEmailEntity));

    public static readonly DomainError InvalidEmail =
        new(1201, "INVALID_EMAIL", nameof(UserEmailEntity));
    #endregion
}
