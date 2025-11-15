using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Application.Dtos;

public class LoginResponseDto
{
    [Required]
    public string Token { get; set; } = string.Empty;
}
