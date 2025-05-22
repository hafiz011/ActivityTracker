using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ActivityTracker.Models.Entities
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string tenant_Id { get; set; }
        public string User_Id { get; set; } 
        public string Email { get; set; }
        public string Last_login { get; set; }
        public string Created_at { get; set; }
    }
}
