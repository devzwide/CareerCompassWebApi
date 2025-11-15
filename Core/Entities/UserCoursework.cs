using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Core.Entities;

public class UserCoursework
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    public required User User { get; set; }

    [Required]
    public int CourseworkId { get; set; }
    public required Coursework Coursework { get; set; }
}