using Sigillum.Arcavis.Core.Application.Abstraction.Outbox;
using Sigillum.Arcavis.Core.Application.CQRS;

namespace Sigillum.Arcavis.Core.Application.Features.Outboxes.Commands.MarkAsFailedAsync;

internal sealed class MarkAsFailedCommandHandler : ICommandHandler<MarkAsFailedCommand>
{
    private readonly IOutboxService _outboxService;

    public MarkAsFailedCommandHandler(IOutboxService outboxService)
    {
        _outboxService = outboxService;
    }

    public async Task Handle(MarkAsFailedCommand request, CancellationToken cancellationToken)
    {
        await _outboxService.MarkAsFailedAsync(request.Id, request.Error, cancellationToken);
    }
}
