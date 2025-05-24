using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ActivityTracker.Models.Entities
{
    public class Tenants
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Org_Name { get; set; }
        public string Domain { get; set; }

        public string ApiSecret { get; set; } // Store hash, not raw secret
        public string[] Scopes { get; set; } = new[] { "read", "write" };

        public string Plan { get; set; }
        public DateTime ExpirationDate { get; set; }

        public int RequestLimit { get; set; }
        public int RequestsMade { get; set; } = 0;
        public DateTime? ResetDate { get; set; }

        public bool IsRevoked { get; set; }
        public bool IsActive { get; set; } = true;

        public DateTime? LastUsed { get; set; }
        public DateTime Created_At { get; set; }

        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string Id { get; set; }
        //public string UserId { get; set; }
        //public string Org_Name { get; set; }
        //public string Domain { get; set; }
        //public string ApiSecret { get; set; }
        //public string Plan { get; set; }
        //public DateTime ExpirationDate { get; set; }
        //public int RequestLimit { get; set; }
        //public bool IsRevoked { get; set; }
        //public DateTime Created_At { get; set; }
    }

}
