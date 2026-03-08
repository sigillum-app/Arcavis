using Sigillum.Arcavis.Core.Application.Abstraction.EventBus;
using Sigillum.Arcavis.Core.Application.Abstraction.Security.Hasher;
using Sigillum.Arcavis.Core.Application.CQRS;
using Sigillum.Arcavis.Core.Domain.Users;

namespace Sigillum.Arcavis.Core.Application.Features.Users.Commands.RegisterUser;

public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, RegisterUserDto>
{
    #region Dependencies
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IEventPublisher _eventPublisher;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IEventPublisher eventPublisher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _eventPublisher = eventPublisher;
    }
    #endregion

    public async Task<RegisterUserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Register(
                        request.Email,
                        _passwordHasher.HashPassword(request.Password)
                   );

        await _userRepository.AddAsync(user);

        foreach (var domainEvent in user.DomainEvents)
            await _eventPublisher.PublishAsync(domainEvent, cancellationToken);

        user.ClearDomainEvents();

        return new RegisterUserDto(user.Id.Value);
    }
}
