using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ActivityTracker.Models.Entities
{
    public class ActivityLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Session_Id { get; set; }
        public string Type { get; set; }
        public string Page_Url { get; set; }
        public DateTime Time_Stamp { get; set; }
        public DateTime LocalTime { get; set; }
    }
}
