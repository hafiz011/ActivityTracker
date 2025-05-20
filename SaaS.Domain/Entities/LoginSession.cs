namespace ActivityTracker.SaaS.Domain.Entities
{
    public class LoginSession
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string TenantId { get; set; }
        public string IPAddress { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string DeviceHash { get; set; }
        public string UserAgent { get; set; }
        public DateTime LoginTimeUtc { get; set; }
        public DateTime? LogoutTimeUtc { get; set; }
    }
}
