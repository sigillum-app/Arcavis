using Sigillum.Arcavis.Core.Application.Contracts.Events;
using Sigillum.Arcavis.Core.Domain.Users.Events;

namespace Sigillum.Arcavis.Core.Application.Features.Users.Commands.RegisterUser.Events.UserRegisteredIntegrationEvent;

public class UserRegisteredIntegrationEventMapper: IIntegrationEventMapper<UserRegisteredEvent>
{
    public Type DomainEventType => typeof(UserRegisteredEvent);

    public string EventName => "user.registered";

    public object Map(object domainEvent)
        => Map((UserRegisteredEvent)domainEvent);

    public Type IntegrationEventType => typeof(UserRegisteredIntegrationEvent);

    public object Map(UserRegisteredEvent e)
    {
        return new UserRegisteredIntegrationEvent(
            e.UserId,
            e.EmailAddress
        );
    }
}
