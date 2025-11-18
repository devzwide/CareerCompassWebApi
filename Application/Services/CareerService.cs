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
        // 1. === GATHER USER'S FULL PROFILE ===

        // Get User and their direct academic info
        var user = await _db.Users
            .AsNoTracking()
            .Include(u => u.UserInterests).ThenInclude(ui => ui.Interest)
            .Include(u => u.FieldOfStudy)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return new List<CareerDto>();
        }

        var userInterestNames = user.UserInterests.Select(ui => ui.Interest.Name.ToLower()).ToHashSet();
        int? userFieldOfStudyId = user.FieldOfStudy?.Id;

        // Get user's coursework IDs
        var userCourseworkIds = await _db.UserCourseworks
            .Where(uc => uc.UserId == userId)
            .Select(uc => uc.CourseworkId)
            .ToHashSetAsync();

        // Get user's explicit skills
        var explicitSkillIds = await _db.UserSkills
            .Where(us => us.UserId == userId)
            .Select(us => us.SkillId)
            .ToHashSetAsync();

        // Get user's IMPLICIT skills from their projects
        var projectSkillIds = await _db.UserProjects
            .Where(up => up.UserId == userId)
            .Select(up => up.ProjectId)
            .Join(_db.ProjectSkills, // Join UserProjects with ProjectSkills
                projectId => projectId,
                projectSkill => projectSkill.ProjectId,
                (projectId, projectSkill) => projectSkill.SkillId)
            .ToHashSetAsync();

        // Get user's IMPLICIT skills from their coursework
        var courseworkSkillIds = await _db.CourseworkSkills
            .Where(cs => userCourseworkIds.Contains(cs.CourseworkId))
            .Select(cs => cs.SkillId)
            .ToHashSetAsync();
        
        // Combine all skills into one unique set
        var combinedUserSkillIds = new HashSet<int>(explicitSkillIds);
        combinedUserSkillIds.UnionWith(projectSkillIds);
        combinedUserSkillIds.UnionWith(courseworkSkillIds);

        
        // 2. === GET ALL CAREERS AND THEIR FULL REQUIREMENTS ===
        
        var careers = await _db.Careers
            .AsNoTracking()
            .Select(c => new 
            {
                Career = c,
                RequiredSkillIds = c.CareerSkills.Select(cs => cs.SkillId).ToHashSet(),
                // Get the new requirements we defined in Step 1
                RequiredFieldOfStudyIds = _db.CareerFieldsOfStudy
                                            .Where(cfos => cfos.CareerId == c.Id)
                                            .Select(cfos => cfos.FieldOfStudyId)
                                            .ToHashSet(),
                RequiredCourseworkIds = _db.CareerCourseworks
                                            .Where(cc => cc.CareerId == c.Id)
                                            .Select(cc => cc.CourseworkId)
                                            .ToHashSet()
            })
            .ToListAsync();

        var careerRecommendations = new List<CareerDto>();

        // 3. === CALCULATE ENHANCED COMPOSITE SCORE ===
        foreach (var item in careers)
        {
            var career = item.Career;
            var dto = new CareerDto
            {
                Id = career.Id,
                Title = career.Title,
                Description = career.Description,
                MarketDemandScore = career.MarketDemandScore,
                SalaryPotentialScore = career.SalaryPotentialScore
            };

            // === Calculate Sub-Scores for ProfileMatch ===

            // a. Skill Score (Weight: 60%)
            double skillScore = 0;
            if (item.RequiredSkillIds.Count > 0)
            {
                int matchingSkillCount = item.RequiredSkillIds.Count(skillId => combinedUserSkillIds.Contains(skillId));
                skillScore = (double)matchingSkillCount / item.RequiredSkillIds.Count;
            }

            // b. Field of Study Score (Weight: 20%)
            double fieldOfStudyScore = 0;
            if (item.RequiredFieldOfStudyIds.Count > 0 && userFieldOfStudyId.HasValue)
            {
                if (item.RequiredFieldOfStudyIds.Contains(userFieldOfStudyId.Value))
                {
                    fieldOfStudyScore = 1.0; // Perfect match
                }
            }

            // c. Coursework Score (Weight: 20%)
            double courseworkScore = 0;
            if (item.RequiredCourseworkIds.Count > 0)
            {
                int matchingCourseworkCount = item.RequiredCourseworkIds.Count(courseId => userCourseworkIds.Contains(courseId));
                courseworkScore = (double)matchingCourseworkCount / item.RequiredCourseworkIds.Count;
            }

            // Combine sub-scores into the ProfileMatchScore
            // (These weights can be adjusted)
            dto.ProfileMatchScore = (skillScore * 0.6) + (fieldOfStudyScore * 0.2) + (courseworkScore * 0.2);

            // Apply interest bonus
            if (userInterestNames.Contains(career.Title.ToLower()))
            {
                dto.ProfileMatchScore *= 1.2; // 20% bonus for expressed interest
            }
            dto.ProfileMatchScore = Math.Min(dto.ProfileMatchScore, 1.0); // Cap at 100%

            // Calculate final Composite Score (same as before)
            dto.CompositeScore = (dto.ProfileMatchScore * 0.5) + (dto.MarketDemandScore * 0.3) + (career.SalaryPotentialScore / 10.0 * 0.2);

            careerRecommendations.Add(dto);
        }

        // 4. === RETURN SORTED LIST ===
        return careerRecommendations
            .OrderByDescending(c => c.CompositeScore)
            .ToList();
    }
}