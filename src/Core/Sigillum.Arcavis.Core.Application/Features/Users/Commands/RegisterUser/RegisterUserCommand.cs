using Mediator;
using Sigillum.Arcavis.Core.Domain.Common;

namespace Sigillum.Arcavis.Core.Application.Features.Users.Commands.RegisterUser;

public record RegisterUserCommand(
    string Username,
    string Email,
    string Password) : ICommand<Result<RegisterUserDto>>;
