using Mediator;
using Sigillum.Arcavis.Core.Application.Abstraction.Outbox;

namespace Sigillum.Arcavis.Core.Application.Features.Outboxes.Commands.MarkAsProcessed;

internal sealed class MarkAsProcessedCommandHandler : ICommandHandler<MarkAsProcessedCommand>
{
    private readonly IOutboxService _outboxService;

    public MarkAsProcessedCommandHandler(IOutboxService outboxService)
    {
        _outboxService = outboxService;
    }

    public async ValueTask<Unit> Handle(MarkAsProcessedCommand request, CancellationToken cancellationToken)
    {
        await _outboxService.MarkAsProcessedAsync(request.Id, cancellationToken);
        
        return Unit.Value;
    }
}
