using Sigillum.Arcavis.Core.Application.Common;

namespace Sigillum.Arcavis.Core.Application.Features.Users.Commands.RegisterUser.Events.UserRegisteredIntegrationEvent;

public record UserRegisteredIntegrationEvent
(
    Guid UserId,
    string EmailAddress
) : IntegrationEvent();
