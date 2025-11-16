using System.Security.Claims;
using CareerCompassWebApi.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCompassWebApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareerController : ControllerBase
    {
        private readonly ICareerService _careerService;
        
        private readonly ISkillGapService _skillGapService;

        public CareerController(ICareerService careerService, ISkillGapService skillGapService)
        {
            _careerService = careerService;
            _skillGapService = skillGapService; 
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

        [HttpGet("recommendations")]
        public async Task<IActionResult> GetRecommendations()
        {
            try
            {
                var userId = GetCurrentUserId();
                var recommendations = await _careerService.GetCareersAsync(userId);
                return Ok(recommendations);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

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
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }
    }
}
