using System;
using WebAPI.Application.Dtos;
using WebAPI.Core.Entities;

namespace WebAPI.Application.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(int id);

    Task<User> UpdateUserProfileAsync(int id, UpdateProfileDto updateDto);
}
