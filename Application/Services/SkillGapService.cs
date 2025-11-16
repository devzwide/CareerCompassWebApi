using System;
using CareerCompassWebApi.Application.Dtos;
using CareerCompassWebApi.Application.Interfaces;
using CareerCompassWebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CareerCompassWebApi.Application.Services;

public class SkillGapService : ISkillGapService
{
    private readonly ApplicationDbContext _db;

    public SkillGapService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<SkillGapAnalysisDto?> AnalyzeSkillGapAsync(int userId, int careerId)
    {
        var career = await _db.Careers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == careerId);

        if (career == null)
        { 
            return null;
        }
        
        var userSkillIds = await _db.UserSkills
            .Where(us => us.UserId == userId)
            .Select(us => us.SkillId)
            .ToHashSetAsync();

        var coreSkillIds = await _db.CareerSkills
            .Where(cs => cs.CareerId == careerId)
            .Select(cs => cs.SkillId)
            .ToHashSetAsync();

        var marketDemandSkills = await _db.JobMarketSkills
            .AsNoTracking()
            .Include(jms => jms.Skill)
            .Where(jms => jms.CareerId == careerId)
            .ToListAsync();

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

        analysis.Skills = analysis.Skills
            .OrderByDescending(s => s.MarketDemand)
            .ToList();

        return analysis;
    }
}
