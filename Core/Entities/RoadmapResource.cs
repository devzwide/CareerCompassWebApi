using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Core.Entities;

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
    public int StepOrder { get; set; } // The order of this step (1, 2, 3...)

    [MaxLength(255)]
    public string? StepTitle { get; set; } // Optional: "Step 1: Learn the Basics"
    
    [MaxLength(1024)]
    public string? StepDescription { get; set; } // Optional: A custom description for this step
}