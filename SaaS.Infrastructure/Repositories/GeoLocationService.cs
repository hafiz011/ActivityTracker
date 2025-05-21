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
        private readonly IMongoCollection<LoginSession> _loginSessions;

        public GeoLocationService(MongoDbContext context, GeolocationService geolocationService)
        {
            _collection = context.LocationDB;
            _geolocationService = geolocationService;
            _loginSessions = context.LoginSessionsDB;
        }

        public async Task<GeoLocationDto> GetLocationAsync(string ipAddress)
        {
            return await _geolocationService.GetGeolocationAsync(ipAddress);
        }

        public async Task InsertLocationAsync(GeoLocation geolocation)
        {
            var filter = Builders<GeoLocation>.Filter.And(
                Builders<GeoLocation>.Filter.Eq(x => x.UserId, geolocation.UserId),
                Builders<GeoLocation>.Filter.Eq(x => x.IpAddress, geolocation.IpAddress),
                Builders<GeoLocation>.Filter.Eq(x => x.TenantId, geolocation.TenantId)
            );

            var sort = Builders<GeoLocation>.Sort.Descending(x => x.CreatedAt);
            var existingRecord = await _collection.Find(filter).Sort(sort).FirstOrDefaultAsync();

            if (existingRecord == null)
            {
                var lastSessionFilter = Builders<GeoLocation>.Filter.And(
                    Builders<GeoLocation>.Filter.Eq(x => x.UserId, geolocation.UserId),
                    Builders<GeoLocation>.Filter.Eq(x => x.TenantId, geolocation.TenantId)
                );

                var lastKnownSession = await _collection.Find(lastSessionFilter).Sort(sort).FirstOrDefaultAsync();

                if (lastKnownSession != null &&
                    lastKnownSession.IpAddress != geolocation.IpAddress &&
                    lastKnownSession.UserId == geolocation.UserId &&
                    lastKnownSession.TenantId == geolocation.TenantId)
                {
                    var data = new LoginSession
                    {
                        TenantId = geolocation.TenantId,
                        UserId = geolocation.UserId,
                        UserNmae = geolocation.UserName,
                        location = geolocation.Id.ToString(),
                        
                    };
                    await _loginSessions.InsertOneAsync(data);
                }
                else
                {
                    await _collection.InsertOneAsync(geolocation);
                }
            }
            else
            {
                bool isMismatch =
                    existingRecord.City != geolocation.City ||
                    existingRecord.Region != geolocation.Region ||
                    existingRecord.Country != geolocation.Country ||
                    existingRecord.Latitude_Longitude != geolocation.Latitude_Longitude ||
                    existingRecord.Isp != geolocation.Isp ||
                    existingRecord.Postal != geolocation.Postal ||
                    existingRecord.TimeZone != geolocation.TimeZone;

                if (isMismatch)
                {
                    existingRecord.City = geolocation.City;
                    existingRecord.Region = geolocation.Region;
                    existingRecord.Country = geolocation.Country;
                    existingRecord.Latitude_Longitude = geolocation.Latitude_Longitude;
                    existingRecord.Isp = geolocation.Isp;
                    existingRecord.Postal = geolocation.Postal;
                    existingRecord.TimeZone = geolocation.TimeZone;

                    await _collection.ReplaceOneAsync(
                        Builders<GeoLocation>.Filter.Eq(x => x.Id, existingRecord.Id),
                        existingRecord
                    );
                }
            }
        }



    }
}
