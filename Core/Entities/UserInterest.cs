using System;
using System.ComponentModel.DataAnnotations;

namespace CareerCompassWebApi.Core.Entities;

public class UserInterest
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    public required User User { get; set; }

    [Required]
    public int InterestId { get; set; }
    public required Interest Interest { get; set; }
}