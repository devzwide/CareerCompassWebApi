using System;
using WebAPI.Application.Dtos;
using WebAPI.Core.Entities;

namespace WebAPI.Application.Interfaces;

public interface IAuthService
{
    // Registers a new user and returns the created User entity
    Task<User> RegisterAsync(RegisterUserDto registerDto);

    // Authenticates a user and returns a LoginResponseDto containing user info and token
    Task<LoginResponseDto> LoginAsync(LoginUserDto loginDto);
}
