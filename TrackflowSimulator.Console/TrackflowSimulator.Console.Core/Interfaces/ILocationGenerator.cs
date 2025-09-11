using Trackflow.Contracts;

namespace TrackflowSimulator.Console.Core.Interfaces
{
    public interface ILocationGenerator
    {
        LocationMessage Generate(int deviceId);
    }
}
