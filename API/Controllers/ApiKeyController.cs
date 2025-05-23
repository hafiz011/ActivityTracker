using ActivityTracker.Application.Interfaces;
using ActivityTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ActivityTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiKeyController : ControllerBase
    {
        private readonly IApiKeyRepository _apiKeyRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApiKeyController(IApiKeyRepository apiKeyRepository, UserManager<ApplicationUser> userManager)
        {
            _apiKeyRepository = apiKeyRepository;
            _userManager = userManager;
        }


        // GET: api/ApiKey
        [HttpGet]
        public async Task<IActionResult> GetApiKey()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { Message = "User not authenticated." });

            var apiKey = await _apiKeyRepository.GetApiByUserIdAsync(userId);
            if (apiKey == null)
                return NotFound(new { Message = "No API key associated with this user." });

            return Ok(new
            {
                apiKey.ApiSecret,
                apiKey.Created_At,
                apiKey.Domain,
                apiKey.Org_Name,
                apiKey.Plan
            });
        }

        public class ApiKeyDto
        {
            public string UserId { get; set; }
            public string Org_Name { get; set; }
            public string Domain { get; set; }
            public string Plan { get; set; }
        }

        // POST: api/ApiKey/Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateApiKey([FromBody] ApiKeyDto apiKeyDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = User.Identity?.Name;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userName))
                return Unauthorized(new { Message = "User not authenticated." });

            var existingKey = await _apiKeyRepository.GetApiByUserIdAsync(userId);
            if (existingKey != null)
                return BadRequest(new { Message = "An API key already exists for your account." });

            var apiKey = await _apiKeyRepository.CreateApiKeyAsync(apiKeyDto);

            return Ok(new
            {
                Message = "API key created successfully.",
                ApiKey = new
                {
                    apiKey.ApiSecret,
                    apiKey.Domain,
                    apiKey.Plan,
                    apiKey.Created_At
                }
            });
        }


        // POST: api/ApiKey/Validate
        [HttpPost("Validate")]
        public async Task<IActionResult> ValidateApiKey([FromBody] string key)
        {
            var apiKey = await _apiKeyRepository.ValidateApiKeyAsync(key);
            if (apiKey == null)
                return BadRequest("Invalid or expired API key.");

            return Ok(new { Message = "API key is valid.", ApiKey = apiKey });
        }

        // POST: api/ApiKey/Renew
        [HttpPost("Renew")]
        public async Task<IActionResult> RenewApiKey([FromBody] string key)
        {
            var success = await _apiKeyRepository.RenewApiKeyAsync(key);
            if (!success)
                return NotFound("API key not found or already revoked.");

            return Ok("API key renewed successfully.");
        }

        // POST: api/ApiKey/Revoke
        [HttpPost("Revoke")]
        public async Task<IActionResult> RevokeApiKey([FromBody] string key)
        {
            var success = await _apiKeyRepository.RevokeApiKeyAsync(key);
            if (!success)
                return NotFound("API key not found.");

            return Ok("API key revoked successfully.");
        }

        // POST: api/ApiKey/TrackUsage
        [HttpPost("TrackUsage")]
        public async Task<IActionResult> TrackUsage([FromBody] string key)
        {
            var success = await _apiKeyRepository.TrackUsageAsync(key);
            if (!success)
                return BadRequest("Invalid API key or request limit exceeded.");

            return Ok("API key usage tracked successfully.");
        }
    }
}
