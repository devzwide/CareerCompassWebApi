using System;

namespace WebAPI.Application.Dtos;

public class UserProfileDto
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";


    public double JobReadyScore { get; set; }

    public DateTime DateOfBirth { get; set; }
    public string? Bio { get; set; }
    public string? ProfilePictureUrl { get; set; }


    public string? PreferredLanguage { get; set; }
    public string? Occupation { get; set; }
    public string? Industry { get; set; }
    public string? Location { get; set; }


    public string? UniversityName { get; set; }
    public string? FieldOfStudyName { get; set; }
    public string? Coursework { get; set; }


    public string? GitHubProfileUrl { get; set; }
    public string? LinkedInProfileUrl { get; set; }


    public ICollection<string> Skills { get; set; } = new List<string>();
    public ICollection<string> Interests { get; set; } = new List<string>();
}
