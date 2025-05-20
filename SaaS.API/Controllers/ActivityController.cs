using ActivityTracker.SaaS.Application.DTOs;
using ActivityTracker.SaaS.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActivityTracker.SaaS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpPost("log")]
        public async Task<IActionResult> LogActivity([FromBody] ActivityLogDto dto)
        {
            await _activityService.LogActivityAsync(dto);
            return Ok();
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserActivities(string userId)
        {
            var logs = await _activityService.GetUserActivitiesAsync(userId);
            return Ok(logs);
        }
    }
}
