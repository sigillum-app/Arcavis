namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Outbox;

public sealed class OutboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;
    public DateTime OccurredAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? Error { get; set; }
    public int RetryCount { get; set; }
    public int MaxRetryCount { get; set; } = 3;
    public DateTime? NextRetryAt { get; set; }

    public OutboxMessage() { }

    public OutboxMessage(string type, string payload, DateTime occurredAt)
    {
        Id = Guid.NewGuid();
        Type = type;
        Payload = payload;
        OccurredAt = occurredAt;
        NextRetryAt = occurredAt;
    }
}