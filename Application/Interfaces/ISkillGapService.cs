using System.Threading.Tasks;
using WebAPI.Application.Dtos;

namespace WebAPI.Application.Interfaces
{
    public interface ISkillGapService
    {
        /// <summary>
        /// Analyzes the gap between a user's skills and market demand for a career.
        /// </summary>
        Task<SkillGapAnalysisDto> AnalyzeSkillGapAsync(int userId, int careerId);
    }
}
