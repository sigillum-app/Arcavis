using Sigillum.Arcavis.Core.Domain.Base;
using Sigillum.Arcavis.Core.Domain.Common;

namespace Sigillum.Arcavis.Core.Domain.Users.Emails;

public sealed class Email
{
    public EmailId Id { get; private set; }
    public string EmailAddress { get; private set; }
    public bool IsVerified { get; private set; }
    public DateTime StartAt { get; private set; }

    protected Email() { }

    private Email(string emailAddress)
    {
        Id = new EmailId(Guid.NewGuid());
        EmailAddress = emailAddress;
        IsVerified = false;
        StartAt = DateTime.UtcNow;
    }

    internal static Result<Email> Create(string emailAddress)
    {
        var validationResult = Validate(emailAddress);

        if (validationResult.IsFailure)
            return Result<Email>.Failure(validationResult.Errors);

        return Result<Email>.Success(new Email(emailAddress.Trim().ToLowerInvariant()));
    }

    internal Result Verify()
    {
        if (IsVerified)
            return Result.Failure(EmailError.AlreadyVerified);

        IsVerified = true;

        return Result.Success();
    }

    internal Result ChangeEmail(string newEmailAddress)
    {
        var validationResult = Validate(newEmailAddress);

        if (validationResult.IsFailure)
            return Result.Failure(validationResult.Errors);

        EmailAddress = newEmailAddress.Trim().ToLowerInvariant();
        IsVerified = false;

        return Result.Success();
    }

    private static Result Validate(string emailAddress)
    {
        if (string.IsNullOrWhiteSpace(emailAddress))
            return Result.Failure(EmailError.InvalidEmail);

        if (!emailAddress.Contains('@'))
            return Result.Failure(EmailError.InvalidEmail);

        return Result.Success();
    }
}
