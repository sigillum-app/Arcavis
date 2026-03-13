//using MassTransit;
//using Sigillum.Arcavis.Core.Application.Abstraction.EventBus;
//using Sigillum.Arcavis.Infrastructure.EventBus.Common;

//namespace Sigillum.Arcavis.Infrastructure.EventBus.Publishers;

//internal sealed class EventPublisher : IEventPublisher
//{
//    private readonly IProducerProvider _producerProvider;

//    public EventPublisher(IProducerProvider producerProvider)
//    {
//        _producerProvider = producerProvider;
//    }

//    public async Task PublishAsync(string type, string payload, CancellationToken cancellationToken = default)
//    {
//        var topic = TopicNameConvention.GetTopicName(type);
//        var producer = _producerProvider.GetProducer<string, string>(topic);
//        await producer.Produce(topic, payload, cancellationToken);
//    }
//}