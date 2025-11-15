using System;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using CareerCompassWebApi.Application.Dtos;
using CareerCompassWebApi.Application.Interfaces;
using CareerCompassWebApi.Core.Entities;
using CareerCompassWebApi.Infrastructure.Data;

namespace CareerCompassWebApi.Application.Services;

public class AuthService : IAuthService
{
    public readonly ApplicationDbContext _context;
    private readonly ITokenService _tokenService;

    public AuthService(ApplicationDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<LoginResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
        {
            throw new Exception("Email is already taken.");
        }

        if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
        {
            throw new Exception("Username is already taken.");
        }

        using var hmac = new HMACSHA512();
        var passwordSalt = hmac.Key;
        var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));

        var user = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return new LoginResponseDto
        {
            Token = _tokenService.CreateToken(user)
        };
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginDto.EmailOrUsername || u.Username == loginDto.EmailOrUsername);
        if (user == null)
        {
            throw new Exception("Invalid credentials.");
        }

        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        bool hashesMatch = CryptographicOperations.FixedTimeEquals(computedHash, user.PasswordHash);

        if (!hashesMatch)
        {
            throw new Exception("Invalid credentials.");
        }

        return new LoginResponseDto
        {
            Token = _tokenService.CreateToken(user)
        };
    }
}