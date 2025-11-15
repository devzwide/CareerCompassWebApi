using System.Security.Claims;
using CareerCompassWebApi.Application.Dtos;
using CareerCompassWebApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCompassWebApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        public readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        private int GetCurrentUserId()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                throw new UnauthorizedAccessException("User ID claim not found in token.");
            }
            return int.Parse(userIdString);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = GetCurrentUserId();
            var user = await _profileService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var profileDto = new ProfileDto
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Bio = user.Bio,
                DateOfBirth = user.DateOfBirth,
                Location = user.Location,
                GitHubProfileUrl = user.GitHubProfileUrl,
                LinkedInProfileUrl = user.LinkedInProfileUrl
            };

            return Ok(profileDto);
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] ProfileEditDto updateDto)
        {
            var userId = GetCurrentUserId();
            var updatedUser = await _profileService.UpdateUserProfileAsync(userId, updateDto);
            if (updatedUser == null)
            {
                return NotFound();
            }

            var profileDto = new ProfileDto
            {
                Id = updatedUser.Id,
                Username = updatedUser.Username,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                Email = updatedUser.Email,
                Bio = updatedUser.Bio,
                DateOfBirth = updatedUser.DateOfBirth,
                Location = updatedUser.Location,
                GitHubProfileUrl = updatedUser.GitHubProfileUrl,
                LinkedInProfileUrl = updatedUser.LinkedInProfileUrl
            };

            return Ok(profileDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _profileService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var profileDto = new ProfileDto
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Bio = user.Bio,
                DateOfBirth = user.DateOfBirth,
                Location = user.Location,
                GitHubProfileUrl = user.GitHubProfileUrl,
                LinkedInProfileUrl = user.LinkedInProfileUrl
            };

            return Ok(profileDto);
        }
    }
}
