using Sigillum.Arcavis.Core.Application.Abstraction.Outbox;
using Sigillum.Arcavis.Core.Application.Common.CQRS;

namespace Sigillum.Arcavis.Core.Application.Features.Outboxes.Commands.MarkAsFailedAsync;

internal sealed class MarkAsFailedCommandHandler : IAppCommandHandler<MarkAsFailedCommand>
{
    private readonly IOutboxService _outboxService;

    public MarkAsFailedCommandHandler(IOutboxService outboxService)
    {
        _outboxService = outboxService;
    }

    public async ValueTask<Unit> Handle(MarkAsFailedCommand request, CancellationToken cancellationToken)
    {
        await _outboxService.MarkAsFailedAsync(request.Id, request.Error, cancellationToken);

        return Unit.Value;
    }
}
