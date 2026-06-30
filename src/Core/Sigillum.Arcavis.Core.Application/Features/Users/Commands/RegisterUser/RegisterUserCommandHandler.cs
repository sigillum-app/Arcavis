using Mediator;
using Sigillum.Arcavis.Core.Application.Contracts.Security.Hasher;
using Sigillum.Arcavis.Core.Domain.Common;
using Sigillum.Arcavis.Core.Domain.Users;

namespace Sigillum.Arcavis.Core.Application.Features.Users.Commands.RegisterUser;

public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Result<RegisterUserDto>>
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

    public async ValueTask<Result<RegisterUserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var result = User.Register(
                request.Email,
                _passwordHasher.HashPassword(request.Password)
           );

        if (result.IsFailure)
            return Result<RegisterUserDto>.Failure(result.Errors);

        await _userRepository.AddAsync(result.Value);

        return Result<RegisterUserDto>.Success(new RegisterUserDto(result.Value.Id.Value));
    }
}
