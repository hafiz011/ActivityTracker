using ActivityTracker.SaaS.Application.DTOs;
using ActivityTracker.SaaS.Application.Interfaces;
using ActivityTracker.SaaS.Domain.Entities;
using ActivityTracker.SaaS.Infrastructure.GeoIPService;
using ActivityTracker.SaaS.Infrastructure.MongoDb;
using MongoDB.Driver;
using System.Net;

namespace ActivityTracker.SaaS.Infrastructure.Repositories
{
    public class GeoLocationService : IGeoLocationService
    {
        private readonly IMongoCollection<GeoLocation> _collection;
        private readonly GeolocationService _geolocationService;


        public GeoLocationService(MongoDbContext context, GeolocationService geolocationService)
        {
            _collection = context.LocationDB;
            _geolocationService = geolocationService;
        }

        public async Task<GeoLocationDto> GetLocationAsync(string ipAddress)
        {
            return await _geolocationService.GetGeolocationAsync(ipAddress);
        }

        public async Task InsartLocationAsync(GeoLocation geolocation)
        {
            var filter = Builders<GeoLocation>.Filter.And(
               Builders<GeoLocation>.Filter.Eq(U => U.UserId, geolocation.UserId),
               Builders<GeoLocation>.Filter.Eq(I => I.IpAddress, geolocation.IpAddress),
               Builders<GeoLocation>.Filter.Eq(T => T.TenantId, geolocation.TenantId) 
           );
            var sort = Builders<GeoLocation>.Sort.Descending(g => g.CreatedAt);
            var data = await _collection.Find(filter).Sort(sort).FirstOrDefaultAsync();

            if (data == null)
            {
                await _collection.InsertOneAsync(geolocation);
                //await _collection.InsertOneAsync(geolocation);
            }

            else
            {
                // Update record if it exists
                bool isMismatch =
                    data.City != geolocation.City ||
                    data.Region != geolocation.Region ||
                    data.Country != geolocation.Country ||
                    data.Latitude_Longitude != geolocation.Latitude_Longitude ||
                    data.Isp != geolocation.Isp ||
                    data.Postal != geolocation.Postal ||
                    data.TimeZone != geolocation.TimeZone;

                if (isMismatch)
                {
                    // Update only if fields mismatch
                    data.City = geolocation.City;
                    data.Region = geolocation.Region;
                    data.Country = geolocation.Country;
                    data.Latitude_Longitude = geolocation.Latitude_Longitude;
                    data.Isp = geolocation.Isp;
                    data.Postal = geolocation.Postal;
                    data.TimeZone = geolocation.TimeZone;
                    await _collection.ReplaceOneAsync(data.Id.ToString(), data);

                }
            }


            //var sort = Builders<GeoLocation>.Sort.Descending(g => g.CreatedAt);

            //return await _userGeolocation.Find(filter).Sort(sort).FirstOrDefaultAsync();
        }
    }
}
