using System;
using WebAPI.Application.Dtos;

namespace WebAPI.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto> RegisterAsync(RegisterUserDto registerDto);

    Task<LoginResponseDto> LoginAsync(LoginUserDto loginDto);
}