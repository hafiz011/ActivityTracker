using ActivityTracker.SaaS.Application.DTOs;
using ActivityTracker.SaaS.Application.Interfaces;
using ActivityTracker.SaaS.Domain.Entities;
using ActivityTracker.SaaS.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace ActivityTracker.SaaS.Infrastructure.Repositories
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
