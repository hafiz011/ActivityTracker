using ActivityTracker.SaaS.Domain.Entities;

namespace ActivityTracker.SaaS.Application.Interfaces
{
    public interface IGeoLocationService
    {
        Task<GeoLocation> GetLocationAsync(string ipAddress);
    }
}
