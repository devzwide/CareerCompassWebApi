using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Core.Entities;

public class User
{
    // Authentication fields
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


    // Profile fields
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

    [MaxLength(2000)]
    public string? AcademicBackground { get; set; }

    [MaxLength(1000)]
    public string? PersonalInterests { get; set; }

    [Url, MaxLength(255)]
    public string? GitHubProfileUrl { get; set; }

    [Url, MaxLength(255)]
    public string? LinkedInProfileUrl { get; set; }


    // Tracking fields
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}
