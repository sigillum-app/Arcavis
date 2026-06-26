using Sigillum.Arcavis.Core.Application.Common.CQRS;

namespace Sigillum.Arcavis.Core.Application.Features.Outboxes.Commands.MarkAsFailedAsync;

public record MarkAsFailedCommand (Guid Id, string Error) : IAppCommand;