using ActivityTracker.SaaS.Application.DTOs;
using ActivityTracker.SaaS.Domain.Entities;

namespace ActivityTracker.SaaS.Application.Interfaces
{
    public interface IActivityService
    {
        Task LogActivityAsync(ActivityLogDto dto);
        Task<IEnumerable<ActivityLog>> GetUserActivitiesAsync(string userId);
    }
}
