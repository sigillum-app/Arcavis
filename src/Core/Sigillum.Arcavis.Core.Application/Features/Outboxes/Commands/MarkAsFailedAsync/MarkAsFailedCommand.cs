using Mediator;

namespace Sigillum.Arcavis.Core.Application.Features.Outboxes.Commands.MarkAsFailedAsync;

public record MarkAsFailedCommand (Guid Id, string Error) : ICommand;