namespace Trackflow.API.Core.Entities
{
    public class Location
    {
        public long Id { get; set; }
        public int DeviceId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
