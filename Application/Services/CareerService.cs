using System;
using CareerCompassWebApi.Application.Dtos;
using CareerCompassWebApi.Application.Interfaces;
using CareerCompassWebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CareerCompassWebApi.Application.Services;

public class CareerService : ICareerService
{
    private readonly ApplicationDbContext _db;

    public CareerService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<CareerDto>> GetCareersAsync(int userId)
    {
        var user = await _db.Users
            .Include(u => u.UserSkills)
            .ThenInclude(us => us.Skill)
            .Include(u => u.UserInterests)
            .ThenInclude(ui => ui.Interest)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return new List<CareerDto>();
        }

        var userSkillIds = user.UserSkills.Select(us => us.SkillId).ToHashSet();
        var userInterestNames = user.UserInterests.Select(ui => ui.Interest.Name).ToHashSet();

        var careers = await _db.Careers
            .Include(c => c.CareerSkills)
            .ThenInclude(cs => cs.Skill)
            .ToListAsync();

        var careerRecommendations = new List<CareerDto>();

        foreach (var career in careers)
        {
            var dto = new CareerDto
            {
                Id = career.Id,
                Title = career.Title,
                Description = career.Description,
                MarketDemandScore = career.MarketDemandScore
            };

            int requiredSkillCount = career.CareerSkills.Count;
            int matchingSkillCount = 0;
            if (requiredSkillCount > 0)
            {
                matchingSkillCount = career.CareerSkills.Count(cs => userSkillIds.Contains(cs.SkillId));
                dto.ProfileMatchScore = (double)matchingSkillCount / requiredSkillCount;
            }

            if (userInterestNames.Contains(career.Title.ToLower()))
            {
                dto.ProfileMatchScore *= 1.2;
            }
            dto.ProfileMatchScore = Math.Min(dto.ProfileMatchScore, 1.0);

            dto.CompositeScore = (dto.ProfileMatchScore * 0.5) + (dto.MarketDemandScore * 0.3) + (career.SalaryPotentialScore / 10.0 * 0.2);

            careerRecommendations.Add(dto);
        }

        return careerRecommendations
            .OrderByDescending(c => c.CompositeScore)
            .ToList();
    }
}
