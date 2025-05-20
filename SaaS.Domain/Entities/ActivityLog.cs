namespace ActivityTracker.SaaS.Domain.Entities
{
    public class ActivityLog
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string TenantId { get; set; }
        public string Action { get; set; }
        public string PageUrl { get; set; }
        //public string IPAddress { get; set; }
        //public string Country { get; set; }
        //public string City { get; set; }
        //public string Device { get; set; }
        public DateTime TimeUtc { get; set; }
        public DateTime LocalTime { get; set; }
    }
}
