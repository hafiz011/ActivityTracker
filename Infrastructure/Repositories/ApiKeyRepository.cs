using ActivityTracker.API.Controllers;
using ActivityTracker.Application.Interfaces;
using ActivityTracker.Infrastructure.MongoDb;
using ActivityTracker.Models.Entities;
using MongoDB.Driver;

namespace ActivityTracker.Infrastructure.Repositories
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        private readonly IMongoCollection<Tenants> _collection;
        public ApiKeyRepository(MongoDbContext context )
        {
            _collection = context.ApiKey;
        }
        public async Task<Tenants> CreateApiKeyAsync(ApiKeyController.ApiKeyDto apiKeyDto)
        {
            var apiKey = new Tenants
            {
                ApiSecret = Guid.NewGuid().ToString("N"),
                UserId = apiKeyDto.UserId,
                Org_Name = apiKeyDto.Org_Name,
                Domain = apiKeyDto.Domain,
                Plan = apiKeyDto.Plan,
                Created_At = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddDays(30),
                RequestLimit = 1000,
                IsRevoked = false
            };

            await _collection.InsertOneAsync(apiKey);
            return apiKey;
        }

        public async Task<IEnumerable<Tenants>> GetAllApiKey(string apiSecret)
        {
            var sort = Builders<Tenants>.Sort.Ascending(x => x.Created_At);
            return await _collection.Find(_ => true).Sort(sort).ToListAsync();
        }

        public async Task<Tenants> GetApiByUserIdAsync(string userId)
        {
            return await _collection.Find(a => a.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<bool> RenewApiKeyAsync(string key)
        {
            var update = Builders<Tenants>.Update.Set(a => a.ExpirationDate, DateTime.UtcNow.AddMonths(1));
            var result = await _collection.UpdateOneAsync(a => a.ApiSecret == key && !a.IsRevoked, update);

            return result.ModifiedCount > 0;
        }

        public async Task<bool> RevokeApiKeyAsync(string key)
        {
            var update = Builders<Tenants>.Update.Set(a => a.IsRevoked, true);
            var result = await _collection.UpdateOneAsync(a => a.ApiSecret == key, update);

            return result.ModifiedCount > 0;
        }

        public async Task<bool> TrackUsageAsync(string key)
        {
            var apiKey = await _collection.Find(a => a.ApiSecret == key).FirstOrDefaultAsync();
            if (apiKey == null || apiKey.IsRevoked || apiKey.RequestLimit <= 0)
            {
                return false;
            }

            var update = Builders<Tenants>.Update.Inc(a => a.RequestLimit, -1);
            var result = await _collection.UpdateOneAsync(a => a.ApiSecret == key && !a.IsRevoked, update);

            return result.ModifiedCount > 0;
        }

        public async Task<Tenants> ValidateApiKeyAsync(string key)
        {
            return await _collection.Find(a => a.ApiSecret == key && a.ExpirationDate > DateTime.UtcNow && !a.IsRevoked).FirstOrDefaultAsync();
        }
    }
}
