using Sigillum.Arcavis.Core.Application.Common.CQRS;

namespace Sigillum.Arcavis.Core.Application.Features.Users.Commands.RegisterUser;

public record RegisterUserCommand(
    string Username,
    string Email,
    string Password) : IAppCommand<RegisterUserDto>;