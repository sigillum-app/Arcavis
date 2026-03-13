namespace Sigillum.Arcavis.Infrastructure.EventBus.Common;

public static class TopicNameConvention
{
    public static string GetTopicName(string eventType)
    {
        var name = eventType.Replace("Event", "");
        return string.Concat(name.Select((c, i) =>
            i > 0 && char.IsUpper(c) ? "." + char.ToLower(c) : char.ToLower(c).ToString()));
    }
}