
using ActivityTracker.Models.Entities;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using static ActivityTracker.API.Controllers.ApiKeyController;

namespace ActivityTracker.Application.Interfaces
{
    public interface IApiKeyRepository
    {
        Task<Tenants> CreateApiKeyAsync(Tenants key);
        Task<Tenants> GetApiByUserIdAsync(string userId);
        Task<IEnumerable<Tenants>> GetAllApiKey(string apiSecret);
        Task<bool> RenewApiKeyAsync(string key);
        Task<bool> RevokeApiKeyAsync(string key);
        Task<bool> TrackUsageAsync(string key);
        Task<Tenants> ValidateApiKeyAsync(string key);
    }
}
