using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Core.Entities;

public class UserRoadmapProgress
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    public required User User { get; set; }

    [Required]
    public int RoadmapResourceId { get; set; }
    public required RoadmapResource RoadmapResource { get; set; }

    [Required]
    public ProgressStatus Status { get; set; } = ProgressStatus.NotStarted;

    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}

public enum ProgressStatus
{
    NotStarted,
    InProgress,
    Completed
}