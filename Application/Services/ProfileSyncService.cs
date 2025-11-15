using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebAPI.Core.Entities;
using WebAPI.Infrastructure.Data;
using System.Linq;

namespace WebAPI.Application.Services;

public class ProfileSyncService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _syncInterval = TimeSpan.FromHours(24);

    public ProfileSyncService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task SyncUserDataAsync(int userId)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var githubService = scope.ServiceProvider.GetRequiredService<GitHubService>();
        var linkedinService = scope.ServiceProvider.GetRequiredService<LinkedInService>();
        var user = await db.Users.FindAsync(userId);
        if (user == null) return;
        double newScore = 0;
        // GitHub: Fetch repos and match to roadmap progress
        if (!string.IsNullOrEmpty(user.GitHubAccessToken))
        {
            var repos = await githubService.GetRepositoriesAsync(user.GitHubAccessToken);
            var roadmapProgresses = await db.UserRoadmapProgresses
                .Include(p => p.RoadmapResource)
                .Where(p => p.UserId == user.Id && p.Status != ProgressStatus.Completed)
                .ToListAsync();
            foreach (var progress in roadmapProgresses)
            {
                var stepTitle = progress.RoadmapResource.StepTitle?.ToLowerInvariant() ?? "";
                foreach (var repo in repos)
                {
                    // Refined matching: check name, description, and topics
                    var repoName = repo.Name.ToLowerInvariant();
                    var repoDesc = repo.Description?.ToLowerInvariant() ?? "";
                    var repoTopics = repo.Topics?.Select(t => t.ToLowerInvariant()) ?? Enumerable.Empty<string>();
                    if (repoName.Contains(stepTitle) || repoDesc.Contains(stepTitle) || repoTopics.Any(t => t.Contains(stepTitle)))
                    {
                        progress.Status = ProgressStatus.Completed;
                        progress.CompletedAt = DateTime.UtcNow;
                        newScore += 10; // +10 points for completed project
                        break;
                    }
                }
            }
        }
        // LinkedIn: Fetch skills and add new ones
        if (!string.IsNullOrEmpty(user.LinkedInAccessToken))
        {
            var skills = await linkedinService.GetSkillsAsync(user.LinkedInAccessToken);
            var dbSkills = await db.Skills.ToListAsync();
            var userSkillIds = await db.UserSkills.Where(us => us.UserId == user.Id).Select(us => us.SkillId).ToListAsync();
            foreach (var skillName in skills)
            {
                // Refined matching: check synonyms
                var match = dbSkills.FirstOrDefault(s => s.Name.Equals(skillName, StringComparison.OrdinalIgnoreCase)
                    || (s.Description != null && s.Description.IndexOf(skillName, StringComparison.OrdinalIgnoreCase) >= 0));
                if (match != null && !userSkillIds.Contains(match.Id))
                {
                    db.UserSkills.Add(new UserSkill
                    {
                        UserId = user.Id,
                        SkillId = match.Id,
                        User = null!,
                        Skill = null!,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    });
                    newScore += 2; // +2 points for new skill (refined)
                }
            }
        }
        // Update JobReadyScore and create notification if changed
        if (user.JobReadyScore != newScore)
        {
            user.JobReadyScore = newScore;
            db.Notifications.Add(new Notification
            {
                UserId = user.Id,
                User = null!,
                Message = $"Your Job Ready score was updated to {newScore} based on your GitHub/LinkedIn profile!",
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            });
        }
        await db.SaveChangesAsync();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var users = await db.Users
                .Where(u => u.GitHubAccessToken != null || u.LinkedInAccessToken != null)
                .ToListAsync(stoppingToken);
            foreach (var user in users)
            {
                await SyncUserDataAsync(user.Id);
            }
            await Task.Delay(_syncInterval, stoppingToken);
        }
    }
}
