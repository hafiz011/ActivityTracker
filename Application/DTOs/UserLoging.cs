namespace ActivityTracker.Application.DTOs
{
    public class UserLoging
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string TenantId { get; set; }
        public string IpAddress { get; set; }
        public string Screen_Resolution { get; set; }
        public string Fingerprint { get; set; }
        public string UserAgent { get; set; }
        public string Session_Id { get; set; } //session collection = id
        public DateTime LocalTime { get; set; }
    }
}
