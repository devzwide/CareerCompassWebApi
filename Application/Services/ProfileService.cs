using System;
using CareerCompassWebApi.Application.Dtos;
using CareerCompassWebApi.Application.Interfaces;
using CareerCompassWebApi.Core.Entities;
using CareerCompassWebApi.Core.Interfaces;

namespace CareerCompassWebApi.Application.Services;

public class ProfileService : IProfileService
{
    private readonly IUserRepository _userRepository;

    public ProfileService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task<User> UpdateUserProfileAsync(int id, ProfileEditDto updateDto)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        user.Username = updateDto.Username ?? user.Username;
        user.FirstName = updateDto.FirstName ?? user.FirstName;
        user.LastName = updateDto.LastName ?? user.LastName;
        user.Bio = updateDto.Bio ?? user.Bio;
        user.Location = updateDto.Location ?? user.Location;
        user.GitHubProfileUrl = updateDto.GitHubProfileUrl ?? user.GitHubProfileUrl;
        user.LinkedInProfileUrl = updateDto.LinkedInProfileUrl ?? user.LinkedInProfileUrl;
        user.UpdatedAt = DateTime.UtcNow;
        
        await _userRepository.UpdateUserAsync(user);
        return user;
    }
}
