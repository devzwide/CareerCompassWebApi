using System;

namespace CareerCompassWebApi.Application.Dtos;

public class SkillGapDto
{
    public string? SkillName { get; set; }
    public bool HasSkill { get; set; }
    public double MarketDemand { get; set; }
    public bool IsCoreRequirement { get; set; }
}
