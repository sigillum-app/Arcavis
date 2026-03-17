using MediatR;
using Sigillum.Arcavis.Core.Application.Abstraction.Security.Hasher;
using Sigillum.Arcavis.Core.Application.CQRS;
using Sigillum.Arcavis.Core.Domain.Users;
using Sigillum.Arcavis.Core.Domain.Users.Events;

namespace Sigillum.Arcavis.Core.Application.Features.Users.Commands.RegisterUser;

public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, RegisterUserDto>
{
    #region Dependencies
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMediator _mediator;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IMediator mediator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mediator = mediator;
    }
    #endregion

    public async Task<RegisterUserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Register(
                        request.Email,
                        _passwordHasher.HashPassword(request.Password)
                   );

        await _userRepository.AddAsync(user);
        await _mediator.Publish(new UserRegisteredNotification(new UserRegisteredEvent(user.Id.Value, user.Emails.FirstOrDefault().EmailAddress)), cancellationToken);

        return new RegisterUserDto(user.Id.Value);
    }
}
