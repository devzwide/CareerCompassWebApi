using System;
using System.ComponentModel.DataAnnotations;

namespace CareerCompassWebApi.Core.Entities;

public class UserCareer
{
    [Key]
    public int Id { get; set; }


    [Required]
    public int UserId { get; set; }
    public required User User { get; set; }


    [Required]
    public int CareerId { get; set; }
    public required Career Career { get; set; }


    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}