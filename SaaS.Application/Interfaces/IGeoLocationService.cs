using ActivityTracker.SaaS.Application.DTOs;
using ActivityTracker.SaaS.Domain.Entities;

namespace ActivityTracker.SaaS.Application.Interfaces
{
    public interface IGeoLocationService
    {
        Task<GeoLocationDto> GetLocationAsync(string ipAddress);
        Task InsertLocationAsync(GeoLocation geolocation);
    }
}
