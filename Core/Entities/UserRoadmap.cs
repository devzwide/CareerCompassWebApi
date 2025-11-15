using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Core.Entities;

public class UserRoadmap
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    public required User User { get; set; }


    [Required]
    public int RoadmapId { get; set; }
    public required Roadmap Roadmap { get; set; }


    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}