using Sigillum.Arcavis.Core.Application.Abstraction.Outbox;
using Sigillum.Arcavis.Core.Application.CQRS;

namespace Sigillum.Arcavis.Core.Application.Features.Outboxes.Commands.MarkAsProcessed;

internal sealed class MarkAsProcessedCommandHandler : ICommandHandler<MarkAsProcessedCommand>
{
    private readonly IOutboxService _outboxService;

    public MarkAsProcessedCommandHandler(IOutboxService outboxService)
    {
        _outboxService = outboxService;
    }

    public async Task Handle(MarkAsProcessedCommand request, CancellationToken cancellationToken)
    {
        await _outboxService.MarkAsProcessedAsync(request.Id, cancellationToken);
    }
}
