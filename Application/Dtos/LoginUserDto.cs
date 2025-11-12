using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Application.Dtos;

public class LoginUserDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = null!; 
    
    [Required] 
    public string Password { get; set; } = null!;
}