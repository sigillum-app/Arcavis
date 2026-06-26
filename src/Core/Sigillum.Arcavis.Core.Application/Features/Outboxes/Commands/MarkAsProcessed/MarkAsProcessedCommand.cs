using Sigillum.Arcavis.Core.Application.Common.CQRS;

namespace Sigillum.Arcavis.Core.Application.Features.Outboxes.Commands.MarkAsProcessed;

public record MarkAsProcessedCommand(Guid Id) : IAppCommand;
