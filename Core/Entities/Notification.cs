using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Core.Entities;

public class Notification
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    public required User User { get; set; }

    [Required, MaxLength(512)]
    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
