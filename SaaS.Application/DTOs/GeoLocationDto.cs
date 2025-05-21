namespace ActivityTracker.SaaS.Application.DTOs
{
    public class GeoLocationDto
    {
        public string IpAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Postal { get; set; }
        public string Latitude_Longitude { get; set; }
        public string Isp { get; set; }
        public string TimeZone { get; set; }
    }
}
