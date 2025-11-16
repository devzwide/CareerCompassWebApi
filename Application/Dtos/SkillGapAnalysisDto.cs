using System;

namespace CareerCompassWebApi.Application.Dtos;

public class SkillGapAnalysisDto
{
    public int CareerId { get; set; }
    public string? CareerTitle { get; set; }
    public List<SkillGapDto> Skills { get; set; } = new List<SkillGapDto>();
}
