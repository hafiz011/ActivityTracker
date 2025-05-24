using ActivityTracker.Application.Interfaces;
using ActivityTracker.Infrastructure.Services;
using ActivityTracker.Models;
using ActivityTracker.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
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
        [HttpGet("GetApiInfo")]
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
                apiKey.Domain,
                apiKey.Org_Name,
                apiKey.Plan,
                apiKey.ExpirationDate,
                apiKey.RequestLimit,
                apiKey.Created_At
            });
        }

        public class ApiKeyDto
        {
            public string Org_Name { get; set; }
            public string Domain { get; set; }
            public string Plan { get; set; }
        }

        // POST: api/ApiKey/Create
        [HttpPost("create")]
        public async Task<IActionResult> CreateApiKey([FromBody] ApiKeyDto apiKeyDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { Message = "User not authenticated." });

            var existingKey = await _apiKeyRepository.GetApiByUserIdAsync(userId);
            if (existingKey != null)
                return BadRequest(new { Message = "An API key already exists for your account." });

            var (rawKey, hashedKey) = ApiKeyGenerator.GenerateApiKey();
            var key = new Tenants
            {
                UserId = userId,
                ApiSecret = hashedKey,
                Org_Name = apiKeyDto.Org_Name,
                Domain = apiKeyDto.Domain,
                Plan = apiKeyDto.Plan,
                Created_At = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddDays(30),
                RequestLimit = 1000,
                IsRevoked = false
            };

            var apiKey = await _apiKeyRepository.CreateApiKeyAsync(key);
            return Ok(new
            {
                Message = "API key created successfully. Please save it now.",
                ApiKey = rawKey
            });
        }

        [HttpPost("regenerate")]
        public async Task<IActionResult> RegenerateApiKey()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { Message = "User not authenticated." });

            var existingKey = await _apiKeyRepository.GetApiByUserIdAsync(userId);
            if (existingKey == null)
                return NotFound(new { Message = "No existing API key found to regenerate." });

            // Revoke old key
            await _apiKeyRepository.RevokeApiKeyAsync(existingKey.ApiSecret);

            // Generate new key
            var (rawKey, hashedKey) = ApiKeyGenerator.GenerateApiKey();

            // Save new key
            var newKey = new Tenants
            {
                UserId = userId,
                ApiSecret = hashedKey,
                Org_Name = existingKey.Org_Name,
                Domain = existingKey.Domain,
                Plan = existingKey.Plan,
                Created_At = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddDays(30),
                RequestLimit = 1000,
                IsRevoked = false
            };

            await _apiKeyRepository.CreateApiKeyAsync(newKey);

            return Ok(new
            {
                Message = "API key regenerated successfully. Please store this key securely.",
                RawApiKey = rawKey,
                newKey.Domain,
                newKey.Org_Name,
                newKey.Plan,
                newKey.ExpirationDate,
                newKey.RequestLimit,
                newKey.Created_At
            });
        }


        public class Renew()
        {
            public string Plan { get; set; }
        }
        // POST: api/ApiKey/Renew
        [HttpPost("renew")]
        public async Task<IActionResult> RenewApiKey([FromBody] Renew renew)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { Message = "User not authenticated." });

            var key = await _apiKeyRepository.GetApiByUserIdAsync(userId);
            if(key == null)
                return NotFound(new { Message = "API key not found." });

            var update = new Tenants
            {
                Plan = renew.Plan,

            };


            // business logic add hare

            var success = await _apiKeyRepository.RenewApiKeyAsync(key.ApiSecret);
            if (!success)
                return NotFound(new { Message = "API key not found or already revoked." });

            return Ok(new { Message = "API key renewed successfully." });
        }

        [Authorize(Roles = "Admin")]
        // POST: api/ApiKey/Revoke
        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeApiKey([FromQuery] string key)
        {
            var success = await _apiKeyRepository.RevokeApiKeyAsync(key);
            if (!success)
                return NotFound(new { Message = "API key not found." });

            return Ok(new { Message = "API key revoked successfully." });
        }


    }
}
