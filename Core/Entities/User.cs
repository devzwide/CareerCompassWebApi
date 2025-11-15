using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Core.Entities;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Username { get; set; } = null!;

    [Required, EmailAddress, MaxLength(100)]
    public string Email { get; set; } = null!;

    [Required]
    public byte[] PasswordHash { get; set; } = null!;

    [Required]
    public byte[] PasswordSalt { get; set; } = null!;



    [Required, MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required, MaxLength(50)]
    public string LastName { get; set; } = null!;

    [Required]
    public DateTime DateOfBirth { get; set; }

    [MaxLength(512)] 
    public string? ProfilePictureUrl { get; set; }

    [MaxLength(2000)] 
    public string? Bio { get; set; }

    [MaxLength(100)]
    public string? Location { get; set; }

    [MaxLength(50)] 
    public string? PreferredLanguage { get; set; }

    [MaxLength(100)] 
    public string? Occupation { get; set; }

    [MaxLength(100)]
    public string? Industry { get; set; }
    
    
    
    public int? UniversityId { get; set; }
    public University? University { get; set; }

    public int? FieldOfStudyId { get; set; }
    public FieldOfStudy? FieldOfStudy { get; set; }

    public ICollection<UserCoursework> UserCourseworks { get; set; } = new List<UserCoursework>();
    public ICollection<UserInterest> UserInterests { get; set; } = new List<UserInterest>();



    [Url, MaxLength(255)]
    public string? GitHubProfileUrl { get; set; }

    [Url, MaxLength(255)]
    public string? LinkedInProfileUrl { get; set; }


    [Required]
    public double JobReadyScore { get; set; } = 0.0;

    // OAuth tokens (should be encrypted at rest)
    [MaxLength(512)]
    public string? GitHubAccessToken { get; set; }

    [MaxLength(512)]
    public string? LinkedInAccessToken { get; set; }

    [MaxLength(512)]
    public string? LinkedInRefreshToken { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }



    public ICollection<UserCareer> UserCareers { get; set; } = new List<UserCareer>();
    public ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
    public ICollection<UserRoadmapProgress> UserRoadmapProgresses { get; set; } = new List<UserRoadmapProgress>();
}