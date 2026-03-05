using Sigillum.Arcavis.Core.Application.CQRS;
using Sigillum.Arcavis.Core.Domain.Users;

namespace Sigillum.Arcavis.Core.Application.Features.User.Commands.RegisterUser;

public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
{
    #region Dependencies
    private readonly IUserRepository _userRepository;

    public RegisterUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    #endregion

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = Domain.Users.User.Register(
                        request.Email,
                        request.Password
                   );

        await _userRepository.AddAsync(user);

        return user.Id.Value;
    }
}
