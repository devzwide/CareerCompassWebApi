using System;
using CareerCompassWebApi.Application.Dtos;
using CareerCompassWebApi.Core.Entities;

namespace CareerCompassWebApi.Application.Interfaces;

public interface IProfileService
{
    Task<User?> GetUserByIdAsync(int id);

    Task<User> UpdateUserProfileAsync(int id, ProfileEditDto updateDto);
}