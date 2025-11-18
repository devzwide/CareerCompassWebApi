using System;
using System.ComponentModel.DataAnnotations;

namespace CareerCompassWebApi.Core.Entities;

public class UserSkill
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    public User? User { get; set; }


    [Required]
    public int SkillId { get; set; }
    public Skill? Skill { get; set; }


    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}