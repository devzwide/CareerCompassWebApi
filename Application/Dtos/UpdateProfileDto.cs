using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Application.Dtos;

public class UpdateProfileDto
{
    [MaxLength(50)]
    public string? Username { get; set; }

    [MaxLength(50)] 
    public string? FirstName { get; set; }
    
    [MaxLength(50)] 
    public string? LastName { get; set; }
    
    [MaxLength(2000)] 
    public string? Bio { get; set; }
    
    [MaxLength(100)] 
    public string? Location { get; set; }
    
    [Url, MaxLength(255)] 
    public string? GitHubProfileUrl { get; set; }
    
    [Url, MaxLength(255)] 
    public string? LinkedInProfileUrl { get; set; }
}
