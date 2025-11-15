using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Core.Entities;

public class Resource
{
    [Key]
    public int Id { get; set; }
    
    [Required, MaxLength(255)]
    public required string Name { get; set; }

    [MaxLength(1024)]
    public string? Description { get; set; }

    [Url, MaxLength(512)]
    public string? ResourceUrl { get; set; }

    [Required]
    public ResourceType ResourceType { get; set; } 

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public enum ResourceType
{
    ProjectIdea,
    Influencer,
    Group,
    Other,

    TechnicalSkill,
    PortfolioProjectIdea,
    NetworkingInfluencer,
    NetworkingGroup,
    OnlinePresenceTip,
    ProfessionalOutreach
}