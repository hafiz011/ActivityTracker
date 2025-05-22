using ActivityTracker.Application.DTOs;
using Newtonsoft.Json;

namespace ActivityTracker.Infrastructure.GeoIPService
{
    public class GeolocationService
    {
        private readonly HttpClient _httpClient;
        private const string AccessToken = "7e3077e36ab204";  //ipinfo.io API key
        public GeolocationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GeoLocationDto> GetGeolocationAsync(string ipAddress)
        {
            string url = $"https://ipinfo.io/{ipAddress}/json?token={AccessToken}";
            var response = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<GeoLocationDto>(response);
        }

    }
}
