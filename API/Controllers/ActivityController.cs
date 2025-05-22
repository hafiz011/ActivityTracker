using ActivityTracker.Application.DTOs;
using ActivityTracker.Application.Interfaces;
using ActivityTracker.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ActivityTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;
        private readonly IGeoLocationService _geoLocationService;

        public ActivityController(IActivityService activityService, IGeoLocationService geoLocationService)
        {
            _activityService = activityService;
            _geoLocationService = geoLocationService;
        }



        [HttpPost("log")]
        public async Task<IActionResult> LogActivity([FromBody] ActivityLogDto dto)
        {
            var location = await _geoLocationService.GetLocationAsync(dto.IPAddress);
            var auth = "test";
            var geolocation = new GeoLocation
            {
                TenantId = auth,
                UserId = dto.UserId,
                UserName = dto.UserName,
                IpAddress = dto.IPAddress,
                Country = location.Country,
                City = location.City,
                Region = location.Region,
                Postal = location.Postal,
                Latitude_Longitude = location.Loc,
                Isp = location.Org,
                TimeZone = location.TimeZone
            };

            await _geoLocationService.InsertLocationAsync(geolocation);
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
