using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.Dtos;
using WebAPI.Application.Interfaces;

namespace WebAPI.Presentation.Presentation
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        public readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
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

        // GET: api/Users/me
        [HttpGet("me")]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = GetCurrentUserId();
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var profileDto = new UserProfileDto
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

        // PUT: api/Users/me
        [HttpPut("me")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateProfileDto updateDto)
        {
            var userId = GetCurrentUserId();
            var updatedUser = await _userService.UpdateUserProfileAsync(userId, updateDto);
            if (updatedUser == null)
            {
                return NotFound();
            }

            var profileDto = new UserProfileDto
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

        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var profileDto = new UserProfileDto
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
