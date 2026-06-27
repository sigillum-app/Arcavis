namespace Sigillum.Arcavis.Core.Application.Features.Users.Commands.RegisterUser;

public record UserRegisteredIntegrationEvent
(
    Guid UserId,
    string EmailAddress,
    DateTime OccurredAt
);