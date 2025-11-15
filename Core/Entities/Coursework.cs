using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Core.Entities;

public class Coursework
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string CourseCode { get; set; } = null!; // e.g., "CS101"

    [Required, MaxLength(255)]
    public string Name { get; set; } = null!; // e.g., "Introduction to Programming"
}