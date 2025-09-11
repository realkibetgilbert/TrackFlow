namespace Trackflow.Contracts
{
    public class LocationMessage
    {
        public int DeviceId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
