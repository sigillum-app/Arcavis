using Sigillum.Arcavis.Core.Domain.Base;
using Sigillum.Arcavis.Core.Domain.Common;
using Sigillum.Arcavis.Core.Domain.Users.Emails;
using Sigillum.Arcavis.Core.Domain.Users.Events;
using Sigillum.Arcavis.Core.Domain.Users.Passwords;

namespace Sigillum.Arcavis.Core.Domain.Users;

public sealed class User : Entity, IAggregateRoot
{
    private readonly List<Email> _emails = new();
    private readonly List<Password> _passwords = new();

    public IReadOnlyCollection<Email> Emails => _emails;
    public IReadOnlyCollection<Password> Passwords => _passwords;

    public UserId Id { get; private set; }
    public bool IsActive { get; private set; }

    protected User() { }

    private User(UserId id)
    {
        Id = id;
        IsActive = false;
    }

    public static Result<User> Register(string emailAddress, string passwordHash)
    {
        var emailResult = Email.Create(emailAddress);
        var passwordResult = Password.Create(passwordHash);

        if (emailResult.IsFailure || passwordResult.IsFailure)
        {
            var errors = emailResult.Errors
                .Concat(passwordResult.Errors)
                .ToList();

            return Result<User>.Failure(errors);
        }

        var user = new User(new UserId(Guid.NewGuid()));
        user._emails.Add(emailResult.Value);
        user._passwords.Add(passwordResult.Value);

        user.AddDomainEvent(new UserRegisteredEvent(user.Id.Value, emailResult.Value.EmailAddress));

        return Result<User>.Success(user);
    }

    public Result Deactivate()
    {
        if (!IsActive)
            return Result.Failure(UserError.UserAlreadyInactive);

        IsActive = false;
        return Result.Success();
    }

    public Result Activate()
    {
        if (IsActive)
            return Result.Failure(UserError.UserAlreadyActive);

        IsActive = true;
        return Result.Success();
    }
}
