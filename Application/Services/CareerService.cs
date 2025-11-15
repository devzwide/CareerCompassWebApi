using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Application.Dtos;
using WebAPI.Application.Interfaces;
using WebAPI.Infrastructure.Data;

namespace WebAPI.Application.Services
{
    public class CareerService : ICareerService
    {
        private readonly ApplicationDbContext _db;

        public CareerService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<CareerRecommendationDto>> GetRecommendationsAsync(int userId)
        {
            // 1. Fetch User's Data
            var user = await _db.Users
                .Include(u => u.UserSkills)
                .ThenInclude(us => us.Skill)
                .Include(u => u.UserInterests)
                .ThenInclude(ui => ui.Interest)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return new List<CareerRecommendationDto>();
            }

            var userSkillIds = user.UserSkills.Select(us => us.SkillId).ToHashSet();
            var userInterestNames = user.UserInterests.Select(ui => ui.Interest.Name.ToLower()).ToHashSet();

            // 2. Fetch All Careers and their requirements
            var allCareers = await _db.Careers
                .Include(c => c.CareerSkills)
                .ThenInclude(cs => cs.Skill)
                .ToListAsync();

            var recommendations = new List<CareerRecommendationDto>();

            // 3. Calculate Scores for each career
            foreach (var career in allCareers)
            {
                var dto = new CareerRecommendationDto
                {
                    CareerId = career.Id,
                    Title = career.Title,
                    Description = career.Description,
                    MarketDemandScore = career.MarketDemandScore,
                    SalaryPotentialScore = career.SalaryPotentialScore
                };

                // --- Start of Scoring Logic ---
                int requiredSkillCount = career.CareerSkills.Count;
                int matchingSkillCount = 0;
                if (requiredSkillCount > 0)
                {
                    matchingSkillCount = career.CareerSkills.Count(cs => userSkillIds.Contains(cs.SkillId));
                    dto.ProfileMatchScore = (double)matchingSkillCount / requiredSkillCount;
                }

                // Boost score based on matching interests
                if (userInterestNames.Contains(career.Title.ToLower()))
                {
                    dto.ProfileMatchScore *= 1.2;
                }
                dto.ProfileMatchScore = Math.Min(dto.ProfileMatchScore, 1.0);

                // Calculate final CompositeScore
                dto.CompositeScore = (dto.ProfileMatchScore * 0.5) + 
                                     (dto.MarketDemandScore * 0.3) + 
                                     (career.SalaryPotentialScore / 10.0 * 0.2);
                // --- End of Scoring Logic ---

                recommendations.Add(dto);
            }

            // 4. Rank and Return
            return recommendations
                .OrderByDescending(r => r.CompositeScore)
                .ToList();
        }
    }
}
