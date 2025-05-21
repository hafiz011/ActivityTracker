namespace ActivityTracker.SaaS.Domain.Entities
{
    public class GeoLocation
    {
        public Guid Id { get; set; }
        public string TenantId { get; set; }
        public string UserId { get; set; }
        public string UserNmae { get; set; }
        public string IpAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Postal { get; set; }
        public string Latitude_Longitude { get; set; }
        public string Isp { get; set; }
        public string TimeZone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;





    }
}
