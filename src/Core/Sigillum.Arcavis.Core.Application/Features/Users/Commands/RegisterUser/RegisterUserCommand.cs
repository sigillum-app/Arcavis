using Mediator;

namespace Sigillum.Arcavis.Core.Application.Features.Users.Commands.RegisterUser;

public record RegisterUserCommand(
    string Username,
    string Email,
    string Password) : ICommand<RegisterUserDto>;