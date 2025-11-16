using CareerCompassWebApi.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCompassWebApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoadmapController : ControllerBase
    {
        private readonly IRoadmapService _roadmapService;

        public RoadmapController(IRoadmapService roadmapService)
        {
            _roadmapService = roadmapService;
        }

        [HttpGet("{careerId}")]
        public async Task<IActionResult> GetRoadmap(int careerId)
        {
            var roadmapDto = await _roadmapService.GetRoadmapForCareerAsync(careerId);
            if (roadmapDto == null)
            {
                return NotFound(new { message = "A roadmap for the specified career could not be found." });
            }
            return Ok(roadmapDto);
        }
    }
}
