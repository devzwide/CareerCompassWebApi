using CareerCompassWebApi.Application.Interfaces;
using CareerCompassWebApi.Core.Entities;
using CareerCompassWebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerCompassWebApi.Application.Services;

public class ProfileEnrichmentService : IProfileEnrichmentService
{
    private readonly ApplicationDbContext _db;
    private readonly GithubService _githubService;
    private readonly LinkedInService _linkedInService;

    public ProfileEnrichmentService(ApplicationDbContext db, GithubService githubService, LinkedInService linkedInService)
    {
        _db = db;
        _githubService = githubService;
        _linkedInService = linkedInService;
    }

    public async Task EnrichFromGitHubAsync(User user)
    {
        if (string.IsNullOrEmpty(user.GitHubAccessToken)) return;

        var repos = await _githubService.GetRepositoriesAsync(user.GitHubAccessToken);
        
        // Get all unique, non-null languages from the user's repos
        var languages = repos
            .Where(r => !string.IsNullOrEmpty(r.Language))
            .Select(r => r.Language)
            .Distinct()
            .ToList();

        if (languages.Any())
        {
            // Pass the list of language strings to our helper
            await AddSkillsToUserAsync(user.Id, languages, SkillType.Language);
        }
    }

    public async Task EnrichFromLinkedInAsync(User user)
    {
        if (string.IsNullOrEmpty(user.LinkedInAccessToken)) return;

        var skills = await _linkedInService.GetSkillsAsync(user.LinkedInAccessToken);

        if (skills.Any())
        {
            // Pass the list of skill strings to our helper
            // We default to 'Other' as LinkedIn doesn't categorize
            await AddSkillsToUserAsync(user.Id, skills, SkillType.Other);
        }
    }

    /// <summary>
    /// This is the core logic. It syncs a list of skill names with the UserSkill table.
    /// It ensures Skills are created if they don't exist and avoids adding duplicate UserSkills.
    /// </summary>
    private async Task AddSkillsToUserAsync(int userId, List<string> skillNames, SkillType defaultType)
    {
        // 1. Get all skills from DB for efficient lookup
        // ToLower() helps normalize data ("c#" vs "C#")
        var allDbSkills = await _db.Skills
            .ToDictionaryAsync(s => s.Name.ToLower(), s => s);

        // 2. Get all skills the user *already* has
        var userCurrentSkillIds = await _db.UserSkills
            .Where(us => us.UserId == userId)
            .Select(us => us.SkillId)
            .ToHashSetAsync();

        var newSkillsToCreate = new List<Skill>();
        var newUserSkillsToAdd = new List<UserSkill>();

        foreach (var name in skillNames)
        {
            var normalizedName = name.Trim();
            var lookupKey = normalizedName.ToLower();
            
            if (string.IsNullOrEmpty(lookupKey)) continue;

            if (!allDbSkills.TryGetValue(lookupKey, out var existingSkill))
            {
                // Skill doesn't exist in our DB. Create it.
                existingSkill = new Skill
                {
                    Name = normalizedName,
                    Type = defaultType,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                newSkillsToCreate.Add(existingSkill);
                
                // Add to lookup dictionary so we don't create it twice in this loop
                allDbSkills[lookupKey] = existingSkill;
            }
        }

        // 3. Save any new skills to the DB *first* so they get an Id
        if (newSkillsToCreate.Any())
        {
            await _db.Skills.AddRangeAsync(newSkillsToCreate);
            await _db.SaveChangesAsync(); // This populates the 'Id' on our existingSkill objects
        }

        // 4. Now link the skills to the user
        foreach (var name in skillNames)
        {
            var lookupKey = name.Trim().ToLower();
            if (string.IsNullOrEmpty(lookupKey)) continue;

            var skill = allDbSkills[lookupKey]; // We know it exists now

            // Check if the user *already* has this skill
            if (!userCurrentSkillIds.Contains(skill.Id))
            {
                // It's a new skill for this user. Add it.
                newUserSkillsToAdd.Add(new UserSkill
                {
                    UserId = userId,
                    SkillId = skill.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
                
                // Add to set to prevent adding twice in one run
                userCurrentSkillIds.Add(skill.Id);
            }
        }

        // 5. Save all the new UserSkill links
        if (newUserSkillsToAdd.Any())
        {
            await _db.UserSkills.AddRangeAsync(newUserSkillsToAdd);
            await _db.SaveChangesAsync();
        }
    }
}