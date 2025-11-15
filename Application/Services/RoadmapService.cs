using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Application.Dtos;
using WebAPI.Application.Interfaces;
using WebAPI.Infrastructure.Data;

namespace WebAPI.Application.Services
{
    public class RoadmapService : IRoadmapService
    {
        private readonly ApplicationDbContext _db;

        public RoadmapService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<RoadmapDto> GetRoadmapForCareerAsync(int careerId)
        {
            var career = await _db.Careers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == careerId);

            if (career == null || career.RoadmapId == 0)
            {
                return null;
            }

            var roadmap = await _db.Roadmaps
                .AsNoTracking()
                .Include(r => r.RoadmapResources)
                    .ThenInclude(rr => rr.Resource)
                .FirstOrDefaultAsync(r => r.Id == career.RoadmapId);

            if (roadmap == null)
            {
                return null;
            }

            var roadmapDto = new RoadmapDto
            {
                Id = roadmap.Id,
                Title = roadmap.Title,
                Description = roadmap.Description,
                Steps = roadmap.RoadmapResources
                    .OrderBy(rr => rr.StepOrder)
                    .Select(rr => new RoadmapStepDto
                    {
                        StepOrder = rr.StepOrder,
                        StepTitle = rr.StepTitle,
                        StepDescription = rr.StepDescription,
                        ResourceName = rr.Resource.Name,
                        ResourceDescription = rr.Resource.Description,
                        ResourceUrl = rr.Resource.ResourceUrl,
                        ResourceType = rr.Resource.ResourceType.ToString()
                    }).ToList()
            };

            return roadmapDto;
        }
    }
}
