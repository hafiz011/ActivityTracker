using ActivityTracker.SaaS.Domain.Entities;
using MongoDB.Bson.IO;

namespace ActivityTracker.SaaS.Infrastructure.GeoIPService
{
    public class GeolocationService
    {
        private readonly HttpClient _httpClient;
        //private const string AccessToken = "7e3077e6_75765765guy_sorry_bro_hare_is_no_tocken_36ab204";
        private const string AccessToken = "you_are_looking_for_a_tocken?_fuck_you";

        public GeolocationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GeoLocation> GetGeolocationAsync(string ipAddress)
        {
            string url = $"https://ipinfo.io/{ipAddress}/json?token={AccessToken}";
            var response = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<GeoLocation>(response);
        }
    }
}
