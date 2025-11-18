using System;
using System.ComponentModel.DataAnnotations;

namespace CareerCompassWebApi.Core.Entities;

public class CourseworkSkill
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int CourseworkId { get; set; }
    public required Coursework Coursework { get; set; }

    [Required]
    public int SkillId { get; set; }
    public required Skill Skill { get; set; }
}
