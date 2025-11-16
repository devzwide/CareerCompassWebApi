using System;

namespace CareerCompassWebApi.Application.Dtos;

public class RoadmapStepDto
{
    public int StepOrder { get; set; }
    public required string StepTitle { get; set; }
    public string? StepDescription { get; set; }
    public required string ResourceName { get; set; }
    public required string ResourceDescription { get; set; }
    public required string ResourceUrl { get; set; }
    public required string ResourceType { get; set; }
}
