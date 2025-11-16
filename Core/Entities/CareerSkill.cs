using System;
using System.ComponentModel.DataAnnotations;

namespace CareerCompassWebApi.Core.Entities;

public class CareerSkill
{
    [Key]
    public int Id { get; set; }


    [Required]
    public int CareerId { get; set; }
    public required Career Career { get; set; }


    [Required]
    public int SkillId { get; set; }
    public required Skill Skill { get; set; }


    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}