using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Application.Dtos;
using WebAPI.Application.Interfaces;
using WebAPI.Infrastructure.Data;

namespace WebAPI.Application.Services
{
    public class SkillGapService : ISkillGapService
    {
        private readonly ApplicationDbContext _db;

        public SkillGapService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<SkillGapAnalysisDto> AnalyzeSkillGapAsync(int userId, int careerId)
        {
            // 1. Get the Career title
            var career = await _db.Careers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == careerId);

            if (career == null) return null;

            // 2. Get the 3 skill sets we need to compare

            // Set A: The user's skills
            var userSkillIds = await _db.UserSkills
                .Where(us => us.UserId == userId)
                .Select(us => us.SkillId)
                .ToHashSetAsync();

            // Set B: The "core" skills for this career (from our roadmap)
            var coreSkillIds = await _db.CareerSkills
                .Where(cs => cs.CareerId == careerId)
                .Select(cs => cs.SkillId)
                .ToHashSetAsync();

            // Set C: The "market demand" skills (from our new table)
            var marketDemandSkills = await _db.JobMarketSkills
                .AsNoTracking()
                .Include(jms => jms.Skill)
                .Where(jms => jms.CareerId == careerId)
                .ToListAsync();

            // 3. Build the analysis
            var analysis = new SkillGapAnalysisDto
            {
                CareerId = careerId,
                CareerTitle = career.Title
            };

            foreach (var marketSkill in marketDemandSkills)
            {
                analysis.Skills.Add(new SkillGapDto
                {
                    SkillName = marketSkill.Skill.Name,
                    MarketDemand = marketSkill.Frequency,
                    HasSkill = userSkillIds.Contains(marketSkill.SkillId),
                    IsCoreRequirement = coreSkillIds.Contains(marketSkill.SkillId)
                });
            }

            // Sort by what's most in-demand
            analysis.Skills = analysis.Skills
                .OrderByDescending(s => s.MarketDemand)
                .ToList();

            return analysis;
        }
    }
}
