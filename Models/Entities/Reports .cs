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
        //filters: {
//    date_range: { start: "2024-06-01", end: "2024-06-10" },
//    device_type: "mobile"
//  },
//  data:
//[
//    { date: "2024-06-01", logins: 45 },
//    { date: "2024-06-02", logins: 52 }
//  ]

    }
}
