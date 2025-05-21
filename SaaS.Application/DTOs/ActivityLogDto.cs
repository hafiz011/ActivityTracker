namespace ActivityTracker.SaaS.Application.DTOs
{
    public class ActivityLogDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public string PageUrl { get; set; }
        public string IPAddress { get; set; }
        public string UserAgent { get; set; }
    }
}
