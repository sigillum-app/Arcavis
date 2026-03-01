namespace Sigillum.Arcavis.Core.Domain.Errors;

public sealed record DomainError
{
    public string Code { get; }

    private DomainError(string code)
    {
        Code = code;
    }

    public static readonly DomainError InvalidCredentials =
        new("INVALID_CREDENTIALS");

    public static readonly DomainError AccountLocked =
        new("ACCOUNT_LOCKED");

    public static readonly DomainError PasswordTooShort =
        new("PASSWORD_TOO_SHORT");

    public static readonly DomainError InvalidTenant =
        new("INVALID_TENANT");

    public static readonly DomainError InvalidUserId =
        new("INVALID_USER_ID");

    public static readonly DomainError InvalidUserPasswordId =
        new("INVALID_USER_PASSWORD_ID");

    public static readonly DomainError InvalidUserPassword =
        new("INVALID_USER_PASSWORD");

    public static readonly DomainError InvalidPassword =
        new("INVALID_PASSWORD");

    public static readonly DomainError InvalidUserEmailId =
        new("INVALID_USER_EMAIL_ID");

    public static readonly DomainError InvalidUserEmail =
        new("INVALID_USER_EMAIL");

    public static readonly DomainError InvalidUser =
        new("INVALID_USER");

    public static readonly DomainError InvalidEmail =
        new("INVALID_EMAIL");

    public static readonly DomainError OldDate =
        new("OLD_DATE");

    public static readonly DomainError SmallerThanExistingLockedDate =
        new("SMALLER_THAN_EXISTING_LOCKED_DATE");
}