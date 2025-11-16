using System;
using System.ComponentModel.DataAnnotations;

namespace CareerCompassWebApi.Core.Entities;

public class RoadmapResource
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int RoadmapId { get; set; }
    public required Roadmap Roadmap { get; set; }

    [Required]
    public int ResourceId { get; set; }
    public required Resource Resource { get; set; }

    [Required]
    public int StepOrder { get; set; } 

    [MaxLength(255)]
    public string? StepTitle { get; set; } 
    
    [MaxLength(1024)]
    public string? StepDescription { get; set; } 
}
