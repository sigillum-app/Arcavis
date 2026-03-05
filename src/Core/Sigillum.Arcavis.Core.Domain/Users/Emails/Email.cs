using Sigillum.Arcavis.Core.Domain.Base;
using Sigillum.Arcavis.Core.Domain.SeedWork.ExceptionHandling;

namespace Sigillum.Arcavis.Core.Domain.Users.Emails;

public sealed class Email : Entity
{
    public EmailId Id { get; private set; }
    public string EmailAddress { get; private set; }
    public bool IsVerified { get; private set; }

    protected Email() { }

    public Email(string emailAddress)
    {
        if (string.IsNullOrWhiteSpace(emailAddress))
            throw new DomainException(EmailErrors.InvalidEmail);

        Id = new EmailId(Guid.NewGuid());
        EmailAddress = emailAddress.Trim().ToLowerInvariant();
        IsVerified = false;
    }

    public void Verify()
    {
        IsVerified = true;
    }

    public void ChangeEmail(string newEmailAddress)
    {
        if (string.IsNullOrWhiteSpace(newEmailAddress))
            throw new DomainException(EmailErrors.InvalidEmail);

        EmailAddress = newEmailAddress.Trim().ToLowerInvariant();
        IsVerified = false;
    }
}
