namespace Sigillum.Arcavis.Core.Application.Common;

public abstract record IntegrationEvent
{
    public Guid EventId { get; init; } = Guid.NewGuid();
    public Guid SourceEventId { get; init; }
    public DateTime OccurredAt { get; init; }
    public string? TraceId { get; init; }
}
