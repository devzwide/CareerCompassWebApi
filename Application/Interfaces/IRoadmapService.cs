using System;
using CareerCompassWebApi.Application.Dtos;

namespace CareerCompassWebApi.Application.Interfaces;

public interface IRoadmapService
{
    Task<RoadmapDto?> GetRoadmapForCareerAsync(int careerId);
}
