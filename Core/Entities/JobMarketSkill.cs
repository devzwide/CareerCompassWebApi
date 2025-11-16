using System;
using System.ComponentModel.DataAnnotations;

namespace CareerCompassWebApi.Core.Entities;

public class JobMarketSkill
{
    [Key]
    public int Id { get; set; }

    public int CareerId { get; set; }
    public required Career Career { get; set; }

    public  int SkillId { get; set; }
    public required Skill Skill { get; set; }

    public double Frequency { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
