using Sigillum.Arcavis.Core.Application.CQRS;

namespace Sigillum.Arcavis.Core.Application.Features.Outboxes.Queries.GetUnprocessedMessages;

public record GetUnprocessedMessagesQuery(int BatchSize = 20) : IQuery<IReadOnlyList<GetUnprocessedMessagesDto>>;
