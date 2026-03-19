namespace Sigillum.Arcavis.Core.Application.IntegrationEvents;

public record UserRegisteredIntegrationEvent
(
    Guid UserId,
    string EmailAddress,
    DateTime OccurredAt
);