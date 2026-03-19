namespace Sigillum.Arcavis.Core.Application.Features.Outboxes.Queries.GetUnprocessedMessages;

public sealed record GetUnprocessedMessagesDto
(
    Guid Id,
    string Type,
    string Payload,
    DateTime OccurredAt,
    DateTime? ProcessedAt,
    string? Error,
    int RetryCount,
    int MaxRetryCount,
    DateTime? NextRetryAt
);
