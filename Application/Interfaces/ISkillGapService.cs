using System;
using CareerCompassWebApi.Application.Dtos;

namespace CareerCompassWebApi.Application.Interfaces;

public interface ISkillGapService
{
    Task<SkillGapAnalysisDto> AnalyzeSkillGapAsync(int userId, int careerId);
}
