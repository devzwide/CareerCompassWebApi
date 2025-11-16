using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCompassWebApi.Core.Entities;

public class Career
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(255)]
    public required string Title { get; set; }

    [MaxLength(1024)]
    public string? Description { get; set; }


    [Required]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal AverageSalary { get; set; }

    [Required]
    public double MarketDemandScore { get; set; } = 5;

    [Required]
    public int SalaryPotentialScore { get; set; } = 5;


    [Required]
    public int RoadmapId { get; set; }
    public Roadmap Roadmap { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


    public ICollection<CareerSkill> CareerSkills { get; set; } = new List<CareerSkill>();
    public ICollection<UserCareer> UserCareers { get; set; } = new List<UserCareer>();
}
