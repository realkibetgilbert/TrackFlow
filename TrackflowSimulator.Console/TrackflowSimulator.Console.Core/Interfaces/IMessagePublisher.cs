using Trackflow.Contracts;

namespace TrackflowSimulator.Console.Core.Interfaces
{
    public interface IMessagePublisher
    {
        Task PublishAsync(LocationMessage message);

    }
}
