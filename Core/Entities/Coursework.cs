using System;
using System.ComponentModel.DataAnnotations;

namespace CareerCompassWebApi.Core.Entities;

public class Coursework
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string CourseCode { get; set; } = null!; 

    [Required, MaxLength(255)]
    public string Name { get; set; } = null!;
}
