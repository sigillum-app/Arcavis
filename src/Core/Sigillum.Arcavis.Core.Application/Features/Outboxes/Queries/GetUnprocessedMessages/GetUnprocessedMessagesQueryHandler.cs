using Sigillum.Arcavis.Core.Application.Abstraction.Persistence.QueryServices;
using Sigillum.Arcavis.Core.Application.Common.CQRS;

namespace Sigillum.Arcavis.Core.Application.Features.Outboxes.Queries.GetUnprocessedMessages;

internal sealed class GetUnprocessedMessagesQueryHandler : IAppQueryHandler<GetUnprocessedMessagesQuery, IReadOnlyList<GetUnprocessedMessagesDto>>
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