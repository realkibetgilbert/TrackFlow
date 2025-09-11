namespace Trackflow.API.Application.DTOs.Location
{
    public class LocationToDisplayDto
    {
        public int DeviceId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
