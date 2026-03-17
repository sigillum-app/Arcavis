using Sigillum.Arcavis.Core.Domain.Base;
using Sigillum.Arcavis.Core.Domain.SeedWork;
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

    public static User Register(string emailAddress, string passwordHash)
    {
        var user = new User(new UserId(Guid.NewGuid()));

        var emailEntity = new Email(emailAddress);
        var passwordEntity = new Password(passwordHash);

        user._emails.Add(emailEntity);
        user._passwords.Add(passwordEntity);
        user.AddDomainEvent(new UserRegisteredEvent(user.Id.Value, emailEntity.EmailAddress));

        return user;
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
    }

    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;
    }
}