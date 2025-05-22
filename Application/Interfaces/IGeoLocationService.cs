using ActivityTracker.Application.DTOs;
using ActivityTracker.Models.Entities;

namespace ActivityTracker.Application.Interfaces
{
    public interface IGeoLocationService
    {
        Task<GeoLocationDto> GetLocationAsync(string ipAddress);
        Task InsertLocationAsync(GeoLocation geolocation);
    }
}
