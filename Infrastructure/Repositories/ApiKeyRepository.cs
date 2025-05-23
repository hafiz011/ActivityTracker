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

        public Task<Tenants> GetAllApiKey(string apiSecret)
        {
            throw new NotImplementedException();
        }

        public Task<Tenants> GetApiByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RenewApiKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RevokeApiKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TrackUsageAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateApiKeyAsync(string key)
        {
            throw new NotImplementedException();
        }
    }
}
