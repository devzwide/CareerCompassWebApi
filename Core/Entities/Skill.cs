using System;
using System.ComponentModel.DataAnnotations;

namespace CareerCompassWebApi.Core.Entities;

public class Skill
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public required string Name { get; set; }

    [Required, MaxLength(50)]
    public required SkillType Type { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;



    public ICollection<CareerSkill> CareerSkills { get; set; } = new List<CareerSkill>();
    public ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
}

public enum SkillType
{
    Language,
    Framework,
    Tool,
    Other
}
