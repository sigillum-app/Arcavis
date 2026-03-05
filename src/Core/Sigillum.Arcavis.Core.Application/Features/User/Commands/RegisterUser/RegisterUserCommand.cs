using Sigillum.Arcavis.Core.Application.CQRS;

namespace Sigillum.Arcavis.Core.Application.Features.User.Commands.RegisterUser;

public record RegisterUserCommand(
    string Username,
    string Email,
    string Password) : ICommand<Guid>;