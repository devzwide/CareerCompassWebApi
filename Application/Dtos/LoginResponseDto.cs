using System;

namespace WebAPI.Application.Dtos;

public class LoginResponseDto
{
    public string? Email { get; set; }
    public string? Username { get; set; }
    public string? Token { get; set; }
}
