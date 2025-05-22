using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ActivityTracker.Models.Entities
{
    public class Reports
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Admin_Id { get; set; }
        public string Type {  get; set; }
        public DateTime Generated_At {  get; set; }
        public Filter Filters { get; set; }

    }
    public class Filter
    {
        public string Date_range { get; set; }
        public string Device_type { get; set; }
    }

}
