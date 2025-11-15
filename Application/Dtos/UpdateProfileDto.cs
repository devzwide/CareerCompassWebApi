using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Application.Dtos;

public class UpdateProfileDto
{
    [MaxLength(50)]
    public string? Username { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Bio { get; set; }


    
    [MaxLength(100)]
    public string? Location { get; set; }

    [MaxLength(100)]
    public string? Occupation { get; set; }

    [MaxLength(100)]
    public string? Industry { get; set; }


    
    [Url, MaxLength(255)] 
    public string? GitHubProfileUrl { get; set; }
    
    [Url, MaxLength(255)]
    public string? LinkedInProfileUrl { get; set; }

    public int? UniversityId { get; set; }
    
    public int? FieldOfStudyId { get; set; }
}
