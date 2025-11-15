using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Core.Entities;

public class Roadmap
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(255)]
    public string? Title { get; set; }

    [MaxLength(1024)]
    public string? Description { get; set; }

    [Required]
    public RoadmapType Type { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<RoadmapResource> RoadmapResources { get; set; } = new List<RoadmapResource>();
}

public enum RoadmapType
{
    TechnicalSkill,
    PortfolioProject,
    Networking,
    Other
}