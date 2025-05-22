
using ActivityTracker.Models.Entities;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using static ActivityTracker.API.Controllers.ApiKeyController;

namespace ActivityTracker.Application.Interfaces
{
    public interface IApiKeyRepository
    {
        Task<Tenants> CreateApiKeyAsync(ApiKeyDto apiKeyDto);
        Task<Tenants> GetApiByUserIdAsync(string userId);
        Task<Tenants> GetAllApiKey(string apiSecret);
        Task<bool> RenewApiKeyAsync(string key);
        Task<bool> RevokeApiKeyAsync(string key);
        Task<bool> TrackUsageAsync(string key);
        Task<bool> ValidateApiKeyAsync(string key);
    }
}
