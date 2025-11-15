using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.Application.Interfaces; // <-- Make sure this is present

namespace WebAPI.Presentation.Presentation
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CareerController : ControllerBase
    {
        private readonly ICareerService _careerService;
        
        // --- START: ADD THESE 2 LINES ---
        private readonly ISkillGapService _skillGapService;

        public CareerController(ICareerService careerService, ISkillGapService skillGapService)
        {
            _careerService = careerService;
            _skillGapService = skillGapService; // Add this
        }
        // --- END: ADD/MODIFY ---

        private int GetCurrentUserId()
        {
            // ... (this method already exists)
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                throw new UnauthorizedAccessException("User ID claim not found in token.");
            }
            return int.Parse(userIdString);
        }

        [HttpGet("recommendations")]
        public async Task<IActionResult> GetRecommendations()
        {
            // ... (this method already exists)
            try
            {
                var userId = GetCurrentUserId();
                var recommendations = await _careerService.GetRecommendationsAsync(userId);
                return Ok(recommendations);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        // --- START: ADD THIS NEW METHOD ---
        /// <summary>
        /// Gets a skill gap analysis for the user against a specific career.
        /// </summary>
        [HttpGet("{careerId}/skillgap")]
        public async Task<IActionResult> GetSkillGap(int careerId)
        {
            try
            {
                var userId = GetCurrentUserId();
                var analysis = await _skillGapService.AnalyzeSkillGapAsync(userId, careerId);

                if (analysis == null)
                {
                    return NotFound(new { message = "Could not perform analysis. Career not found." });
                }

                return Ok(analysis);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }
        // --- END: ADD THIS NEW METHOD ---
    }
}
