using System.Threading.Tasks;
using WebAPI.Application.Dtos;

namespace WebAPI.Application.Interfaces
{
    public interface IRoadmapService
    {
        /// <summary>
        /// Gets the detailed, step-by-step roadmap for a given career ID.
        /// </summary>
        /// <param name="careerId">The ID of the career.</param>
        /// <returns>A RoadmapDto containing all steps, or null if not found.</returns>
        Task<RoadmapDto> GetRoadmapForCareerAsync(int careerId);
    }
}
