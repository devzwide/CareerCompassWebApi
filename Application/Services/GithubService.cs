using System;
using Octokit;

namespace CareerCompassWebApi.Application.Services;

public class GithubService
{
    public async Task<IReadOnlyList<Repository>> GetRepositoriesAsync(string accessToken)
    {
        if (string.IsNullOrEmpty(accessToken))
        {
            return new List<Repository>();
        }

        var client = new GitHubClient(new ProductHeaderValue("CareerCompass"));
        client.Credentials = new Credentials(accessToken);

        try
        {
            var repositories = await client.Repository.GetAllForCurrent();
            return repositories;
        }
        catch (ApiException ex)
        {
            Console.WriteLine($"GitHub API Error: {ex.Message}");
            return new List<Repository>();
        }
    }
}
