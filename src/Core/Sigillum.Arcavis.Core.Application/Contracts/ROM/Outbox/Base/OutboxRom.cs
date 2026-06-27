namespace Sigillum.Arcavis.Core.Application.Contracts.ROM.Outbox.Base;

public class OutboxRom
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public string Payload { get; set; }
    public DateTime OccurredAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? Error { get; set; }
    public int RetryCount { get; set; }
    public int MaxRetryCount { get; set; }
    public DateTime? NextRetryAt { get; set; }
}
