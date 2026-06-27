using Mediator;
using Sigillum.Arcavis.Core.Application.Contracts.EventBus;
using Sigillum.Arcavis.Core.Application.Contracts.Outbox;
using Sigillum.Arcavis.Core.Application.Contracts.Persistence;
using Sigillum.Arcavis.Core.Application.Contracts.Persistence.QueryServices;

namespace Sigillum.Arcavis.Core.Application.Features.Outboxes.Commands.ProcessMessages;

internal sealed class ProcessMessagesCommandHandler : ICommandHandler<ProcessMessagesCommand>
{
    #region Dependencies
    private readonly IOutboxQueryService _outboxQueryService;
    private readonly IOutboxService _outboxService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;

    public ProcessMessagesCommandHandler(
        IOutboxQueryService outboxQueryService,
        IOutboxService outboxService,
        IUnitOfWork unitOfWork,
        IEventPublisher eventPublisher)
    {
        _outboxQueryService = outboxQueryService;
        _outboxService = outboxService;
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
    }
    #endregion

    public async ValueTask<Unit> Handle(ProcessMessagesCommand request, CancellationToken cancellationToken)
    {
        var messages = await _outboxQueryService.GetUnprocessedMessagesAsync(20, cancellationToken);

        foreach (var message in messages)
        {
            try
            {
                await _eventPublisher.PublishAsync(message.Type, message.Payload, cancellationToken);
                await _outboxService.MarkAsProcessedAsync(message.Id, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await _outboxService.MarkAsFailedAsync(message.Id, ex.Message, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        return Unit.Value;
    }
}
