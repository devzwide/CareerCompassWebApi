using System;
using WebAPI.Core.Entities;

namespace WebAPI.Core.Interfaces;

public interface IUserRepository
{
    // CREATE
    // Adds a new user to the repository
    Task AddUserAsync(User user);

    // READ 
    // Retrieves all users from the repository
    Task<User?> GetUserByIdAsync(int id);

    // UPDATE
    // Updates an existing user in the repository
    Task UpdateUserAsync(User user);

    // DELETE
    // Deletes a user from the repository by their ID
    Task DeleteUserAsync(int id);   
}
