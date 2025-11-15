using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Core.Entities;

public class Project
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [Url, MaxLength(512)]
    public string? RepositoryUrl { get; set; }

    // Timestamps
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}