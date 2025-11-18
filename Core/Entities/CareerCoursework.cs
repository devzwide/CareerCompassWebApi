using System;
using System.ComponentModel.DataAnnotations;

namespace CareerCompassWebApi.Core.Entities;

public class CareerCoursework
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int CareerId { get; set; }
    public required Career Career { get; set; }

    [Required]
    public int CourseworkId { get; set; }
    public required Coursework Coursework { get; set; }
}