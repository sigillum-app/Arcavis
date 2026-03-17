using MediatR;
using Sigillum.Arcavis.Core.Application.Abstraction.Outbox;
using Sigillum.Arcavis.Core.Domain.Users.Events;
using System.Text.Json;

namespace Sigillum.Arcavis.Core.Application.Features.Users.Commands.RegisterUser;

public sealed class UserRegisteredNotificationHandler : INotificationHandler<UserRegisteredNotification>
{
    private readonly IOutboxService _outboxService;

    public UserRegisteredNotificationHandler(IOutboxService outboxService)
    {
        _outboxService = outboxService;
    }

    public async Task Handle(UserRegisteredNotification notification, CancellationToken cancellationToken)
    {
        await _outboxService.AddAsync(
            typeof(UserRegisteredEvent).AssemblyQualifiedName,
            JsonSerializer.Serialize(notification.DomainEvent),
            notification.DomainEvent.OccurredAt,
            cancellationToken);
    }
}
