using Sigillum.Arcavis.Core.Application.Common.CQRS;

namespace Sigillum.Arcavis.Core.Application.Features.Outboxes.Queries.GetUnprocessedMessages;

public record GetUnprocessedMessagesQuery(int BatchSize = 20) : IAppQuery<IReadOnlyList<GetUnprocessedMessagesDto>>;
