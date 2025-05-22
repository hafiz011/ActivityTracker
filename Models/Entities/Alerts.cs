using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ActivityTracker.Models.Entities
{
    public class Alerts
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string User_Id { get; set; } // user collection id
        public string Type { get; set; }
        public string Servity { get; set; }
        public DateTime triggered_at { get; set; }
        public bool is_resolved { get; set; }
        public Context context { get; set; }

    }
    public class Context
    {
        public string From_Country { get; set; }
        public string To_Country { get; set; }
        public string time_elapsed_minutes { get; set; }

    }
}
