using System;
using System.ComponentModel.DataAnnotations;

namespace CareerCompassWebApi.Core.Entities;

public class ProjectSkill
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ProjectId { get; set; }
    public required Project Project { get; set; }

    [Required]
    public int SkillId { get; set; }
    public required Skill Skill { get; set; }
}
