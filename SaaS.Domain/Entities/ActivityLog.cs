using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ActivityTracker.SaaS.Domain.Entities
{
    public class ActivityLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string TenantId { get; set; }
        public string Action { get; set; }
        public string PageUrl { get; set; }

        public DateTime TimeUtc { get; set; }
        public DateTime LocalTime { get; set; }
    }
}
