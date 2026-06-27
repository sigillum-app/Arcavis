using Mediator;
using Sigillum.Arcavis.Core.Application.Contracts.Persistence.QueryServices;

namespace Sigillum.Arcavis.Core.Application.Features.Outboxes.Queries.GetUnprocessedMessages;

internal sealed class GetUnprocessedMessagesQueryHandler : IQueryHandler<GetUnprocessedMessagesQuery, IReadOnlyList<GetUnprocessedMessagesDto>>
{
    private readonly IOutboxQueryService _queryService;

    public GetUnprocessedMessagesQueryHandler(IOutboxQueryService queryService)
    {
        _queryService = queryService;
    }

    public async ValueTask<IReadOnlyList<GetUnprocessedMessagesDto>> Handle(GetUnprocessedMessagesQuery request, CancellationToken cancellationToken)
    {
        var roms = await _queryService.GetUnprocessedMessagesAsync(request.BatchSize, cancellationToken);

        return roms.Select(x => new GetUnprocessedMessagesDto(
            x.Id,
            x.Type,
            x.Payload,
            x.OccurredAt,
            x.ProcessedAt,
            x.Error,
            x.RetryCount,
            x.MaxRetryCount,
            x.NextRetryAt
        )).ToList().AsReadOnly();
    }
}