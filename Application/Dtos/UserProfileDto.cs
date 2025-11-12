using System;

namespace WebAPI.Application.Dtos;

public class UserProfileDto
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? Bio { get; set; }
    public string? Location { get; set; }
    public string? GitHubProfileUrl { get; set; }
    public string? LinkedInProfileUrl { get; set; }
}
