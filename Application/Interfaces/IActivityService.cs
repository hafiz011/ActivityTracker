using ActivityTracker.Models.Entities;
using ActivityTracker.Application.DTOs;

namespace ActivityTracker.Application.Interfaces
{
    public interface IActivityService
    {
        Task LogActivityAsync(UserLoging dto);
        Task<IEnumerable<ActivityLog>> GetUserActivitiesAsync(string userId);
    }
}
