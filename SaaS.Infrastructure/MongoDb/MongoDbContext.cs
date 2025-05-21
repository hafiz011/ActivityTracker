using ActivityTracker.SaaS.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ActivityTracker.SaaS.Infrastructure.MongoDb
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<ActivityLog> ActivityDB => _database.GetCollection<ActivityLog>("ActivityDB");
        public IMongoCollection<GeoLocation> LocationDB => _database.GetCollection<GeoLocation>("LocationDB");


    }
}
