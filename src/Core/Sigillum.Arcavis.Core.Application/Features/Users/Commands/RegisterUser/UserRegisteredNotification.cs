using MediatR;
using Sigillum.Arcavis.Core.Domain.Users.Events;

namespace Sigillum.Arcavis.Core.Application.Features.Users.Commands.RegisterUser;

public sealed record UserRegisteredNotification(UserRegisteredEvent DomainEvent) : INotification;

