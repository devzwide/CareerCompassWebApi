using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.Application.Interfaces;

namespace WebAPI.Presentation.Presentation
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlumniController : ControllerBase
    {
        private readonly IAlumniService _alumniService;

        public AlumniController(IAlumniService alumniService)
        {
            _alumniService = alumniService;
        }

        private int GetCurrentUserId()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(userIdString ?? "0");
        }

        /// <summary>
        /// Search for alumni from the user's university.
        /// </summary>
        /// <param name="keyword">Optional keyword to filter by role or industry.</param>
        [HttpGet("search")]
        public async Task<IActionResult> SearchAlumni([FromQuery] string? keyword)
        {
            var userId = GetCurrentUserId();
            var results = await _alumniService.FindAlumniAsync(userId, keyword);
            return Ok(results);
        }

        /// <summary>
        /// Get a warm introduction message template for a specific alumni.
        /// </summary>
        [HttpGet("{alumniId}/intro-template")]
        public async Task<IActionResult> GetIntroTemplate(int alumniId)
        {
            var userId = GetCurrentUserId();
            var template = await _alumniService.GenerateIntroTemplateAsync(userId, alumniId);

            if (template == null)
            {
                return NotFound(new { message = "Alumni not found." });
            }

            return Ok(template);
        }
    }
}
