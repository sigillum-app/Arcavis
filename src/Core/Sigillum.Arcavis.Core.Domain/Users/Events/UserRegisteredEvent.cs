using Sigillum.Arcavis.Core.Domain.Common;

namespace Sigillum.Arcavis.Core.Domain.Users.Events;

public sealed record UserRegisteredEvent
(
    Guid UserId,
    string EmailAddress
) : DomainEvent;
