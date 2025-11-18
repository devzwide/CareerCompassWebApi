using CareerCompassWebApi.Core.Entities;

namespace CareerCompassWebApi.Application.Interfaces;

public interface IProfileEnrichmentService
{
    // Fetches repo languages and maps them to UserSkills
    Task EnrichFromGitHubAsync(User user);

    // Fetches LinkedIn skills and maps them to UserSkills
    Task EnrichFromLinkedInAsync(User user);
}