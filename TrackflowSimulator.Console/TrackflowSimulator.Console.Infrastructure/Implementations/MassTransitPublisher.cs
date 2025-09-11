using MassTransit;
using Trackflow.Contracts;
using TrackflowSimulator.Console.Core.Interfaces;

namespace TrackflowSimulator.Console.Infrastructure.Implementations
{
    public class MassTransitPublisher : IMessagePublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MassTransitPublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishAsync(LocationMessage message)
        {
            await _publishEndpoint.Publish(message);
        }
    }
}
