
using Mediator;

namespace Sigillum.Arcavis.Core.Application.Features.Outboxes.Commands.MarkAsProcessed;

public record MarkAsProcessedCommand(Guid Id) : ICommand;
