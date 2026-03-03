using Sigillum.Arcavis.Core.Domain.Entities;
using Sigillum.Arcavis.Core.Domain.ExceptionHandling;

namespace Sigillum.Arcavis.Core.Domain.Errors;

public static class UserEntityErrors
{
    #region 1100 - 1199 => User Entity Errors
    public static readonly DomainError InvalidUserId =
        new(1100, "INVALID_USER_ID", nameof(UserEntity));
    #endregion
}
