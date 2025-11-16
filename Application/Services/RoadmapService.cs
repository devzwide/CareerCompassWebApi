using System;
using CareerCompassWebApi.Application.Dtos;
using CareerCompassWebApi.Application.Interfaces;
using CareerCompassWebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CareerCompassWebApi.Application.Services;

public class RoadmapService : IRoadmapService
{
    private readonly ApplicationDbContext _db;

    public RoadmapService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<RoadmapDto?> GetRoadmapForCareerAsync(int careerId)
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
            Title = roadmap.Title ?? string.Empty,
            Description = roadmap.Description ?? string.Empty,
            Steps = roadmap.RoadmapResources
                .OrderBy(rr => rr.StepOrder)
                .Select(rr => new RoadmapStepDto
                {
                    StepOrder = rr.StepOrder,
                    StepTitle = rr.StepTitle ?? string.Empty,
                    StepDescription = rr.StepDescription ?? string.Empty,
                    ResourceName = rr.Resource?.Name ?? string.Empty,
                    ResourceDescription = rr.Resource?.Description ?? string.Empty,
                    ResourceUrl = rr.Resource?.ResourceUrl ?? string.Empty,
                    ResourceType = rr.Resource?.ResourceType.ToString() ?? string.Empty
                }).ToList()
        };

        return roadmapDto;
    }
}
