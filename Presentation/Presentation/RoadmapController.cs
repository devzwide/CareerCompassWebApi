using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Application.Interfaces;

namespace WebAPI.Presentation.Presentation
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
