using Octokit;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WebAPI.Application.Services
{
    // We'll eventually create an IGitHubService interface for this,
    // but for now, we can build the implementation.
    public class GitHubService
    {
        /// <summary>
        /// Fetches the repositories for the user authenticated with the given access token.
        /// </summary>
        /// <param name="accessToken">The user's GitHub OAuth access token.</param>
        /// <returns>A read-only list of repository objects from Octokit.</returns>
        public async Task<IReadOnlyList<Repository>> GetRepositoriesAsync(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                return new List<Repository>();
            }

            // Create a new GitHub client and authenticate with the user's token
            var client = new GitHubClient(new ProductHeaderValue("CareerCompass"));
            client.Credentials = new Credentials(accessToken);

            try
            {
                // Fetch and return all repositories for the currently authenticated user
                var repositories = await client.Repository.GetAllForCurrent();
                return repositories;
            }
            catch (ApiException ex)
            {
                // Log the exception (e.g., if the token is invalid or expired)
                Console.WriteLine($"GitHub API Error: {ex.Message}");
                // Return an empty list on failure
                return new List<Repository>();
            }
        }
    }
}
