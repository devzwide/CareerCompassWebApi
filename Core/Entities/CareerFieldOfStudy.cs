using System;
using System.ComponentModel.DataAnnotations;

namespace CareerCompassWebApi.Core.Entities;

public class CareerFieldOfStudy
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int CareerId { get; set; }
    public required Career Career { get; set; }

    [Required]
    public int FieldOfStudyId { get; set; }
    public required FieldOfStudy FieldOfStudy { get; set; }
}