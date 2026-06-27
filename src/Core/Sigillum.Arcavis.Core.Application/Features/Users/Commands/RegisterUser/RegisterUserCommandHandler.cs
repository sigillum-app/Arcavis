using Mediator;
using Sigillum.Arcavis.Core.Application.Contracts.Security.Hasher;
using Sigillum.Arcavis.Core.Domain.Users;

namespace Sigillum.Arcavis.Core.Application.Features.Users.Commands.RegisterUser;

public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, RegisterUserDto>
{
    #region Dependencies
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }
    #endregion

    public async ValueTask<RegisterUserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Register(
                request.Email,
                _passwordHasher.HashPassword(request.Password)
           );

        await _userRepository.AddAsync(user);

        return new RegisterUserDto(user.Id.Value);
    }
}
