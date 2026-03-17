namespace Sigillum.Arcavis.Infrastructure.Persistence.EfCore.Outbox;

public sealed class OutboxMessage
{
    public Guid Id { get; private set; }
    public string Type { get; private set; }
    public string Payload { get; private set; }
    public DateTime OccurredAt { get; private set; }
    public DateTime? ProcessedAt { get; private set; }
    public string? Error { get; private set; }
    public int RetryCount { get; private set; }
    public int MaxRetryCount { get; private set; }
    public DateTime? NextRetryAt { get; private set; }

    private OutboxMessage() { }

    public OutboxMessage(string type, string payload, DateTime occurredAt)
    {
        Id = Guid.NewGuid();
        Type = type;
        Payload = payload;
        OccurredAt = occurredAt;
        NextRetryAt = DateTime.UtcNow;
        MaxRetryCount = 3;
    }
}