using System;
using System.ComponentModel.DataAnnotations;

namespace CareerCompassWebApi.Application.Dtos;

public class LoginDto
{
    [Required]
    public string EmailOrUsername { get; set; } = null!;
    
    [Required] 
    public string Password { get; set; } = null!;
}