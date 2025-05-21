using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ActivityTracker.SaaS.Domain.Entities
{
    public class LoginSession
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string TenantId { get; set; }
        public string UserId { get; set; }
        public string UserNmae { get; set; }
        public string location { get; set; }
        public DeviceInfo Device { get; set; }
        public DateTime LoginTimeUtc { get; set; } = DateTime.UtcNow;
        public DateTime? LogoutTimeUtc { get; set; }
    }
    public class DeviceInfo
    {
        public string UserAgent { get; set; }
        public string Platform { get; set; }
        public string Language { get; set; }
        public string ScreenResolution { get; set; }
        public string DeviceHash { get; set; }
    }
}
