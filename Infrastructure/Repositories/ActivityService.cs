using ActivityTracker.Application.DTOs;
using ActivityTracker.Application.Interfaces;
using ActivityTracker.Models.Entities;
using ActivityTracker.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace ActivityTracker.Infrastructure.Repositories
{
    public class ActivityService : IActivityService
    {
        private readonly IMongoCollection<ActivityLog> _collection;


        public ActivityService(MongoDbContext context)
        {
            _collection = context.ActivityDB;
        }

        public async Task LogActivityAsync(ActivityLogDto dto)
        {
            var log = new ActivityLog
            {
                UserId = dto.UserId,
                Action = dto.Action,
                PageUrl = dto.PageUrl,
                //IPAddress = dto.IPAddress,
                //Device = dto.UserAgent,
                TimeUtc = DateTime.UtcNow,
                LocalTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.Local)
            };
            await _collection.InsertOneAsync(log);
        }

        public async Task<IEnumerable<ActivityLog>> GetUserActivitiesAsync(string userId)
        {
            return await _collection.Find(x => x.UserId == userId).ToListAsync();
        }



    }
}
