using System;
using CareerCompassWebApi.Core.Entities;

namespace CareerCompassWebApi.Core.Interfaces;

public interface IUserRepository
{
    Task AddUserAsync(User user);

    Task<User?> GetUserByIdAsync(int id);

    Task UpdateUserAsync(User user);

    Task DeleteUserAsync(int id);   
}