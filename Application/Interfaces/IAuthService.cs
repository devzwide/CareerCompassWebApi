using System;
using CareerCompassWebApi.Application.Dtos;

namespace CareerCompassWebApi.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
}
