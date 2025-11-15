using System;
using System.ComponentModel.DataAnnotations;

namespace CareerCompassWebApi.Application.Dtos;

public class RegisterDto
{
    [Required, MaxLength(50)] 
    public string Username { get; set; } = null!; 
    
    [Required, EmailAddress, MaxLength(100)] 
    public string Email { get; set; } = null!; 
    
    [Required, MinLength(8)] 
    public string Password { get; set; } = null!; 
    
    [Required, MaxLength(50)] 
    public string FirstName { get; set; } = null!; 
    
    [Required, MaxLength(50)] 
    public string LastName { get; set; } = null!;
}
