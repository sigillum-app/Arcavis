using Sigillum.Arcavis.Core.Application.Common;

namespace Sigillum.Arcavis.Core.Application.Features.Users.Commands.RegisterUser;

public static class RegisterUserError
{
    #region 1000 - 1019 => RegisterUser Command Error

    #endregion

    #region 1000 - 1019 => RegisterUser Command Validations Error
    public static readonly ValidationError PasswordIsTooShort = 
        new ValidationError(1001, "PASSWORD_IS_TOO_SHORT", nameof(RegisterUserCommand));
    #endregion
}
