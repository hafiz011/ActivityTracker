using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ActivityTracker.Models.Entities
{
    public class Sessions
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Tenant_Id { get; set; }
        public string User_Id { get; set; } //user collection id
        public string Ip_Address { get; set; }
        public DeviceInfo Device { get; set; }
        public Location Geo_Location { get; set; }
        public DateTime Login_Time { get; set; } = DateTime.UtcNow;
        public DateTime? Logout_Time { get; set; }
        public bool isSuspicious { get; set; }
        public string suspicious_Flags { get; set; }
    }
    public class DeviceInfo
    {
        public string Fingerprint { get; set; }
        public string Browser { get; set; }
        public string Device_Type { get; set; }
        public string OS { get; set; }
        public string Language { get; set; }
        public string Screen_Resolution { get; set; }
    }
    public class Location
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Postal { get; set; }
        public string Latitude_Longitude { get; set; }
        public string Isp { get; set; }
        public string TimeZone { get; set; }
        public bool is_vpn { get; set; }
    }
}
