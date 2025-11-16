using System;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CareerCompassWebApi.Application.Services;

public class LinkedInService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public LinkedInService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<string>> GetSkillsAsync(string accessToken)
    {
        if (string.IsNullOrEmpty(accessToken))
        {
            return new List<string>();
        }

        var client = _httpClientFactory.CreateClient("LinkedIn");

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        client.DefaultRequestHeaders.Add("Accept", "application/json");

        var url = "https://api.linkedin.com/v2/me?projection=(skills)";

        try
        {
            var response = await client.GetAsync(url);
            
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"LinkedIn API Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                return new List<string>();
            }

            var content = await response.Content.ReadAsStringAsync();
            return ParseSkillsFromJson(content);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"LinkedIn Service Exception: {ex.Message}");
            return new List<string>();
        }
    }

    private List<string> ParseSkillsFromJson(string jsonContent)
    {
        var skills = new List<string>();
        try
        {
            using (JsonDocument doc = JsonDocument.Parse(jsonContent))
            {
                JsonElement root = doc.RootElement;
                if (root.TryGetProperty("skills", out JsonElement skillsElement) &&
                    skillsElement.TryGetProperty("elements", out JsonElement elements))
                {
                    foreach (JsonElement element in elements.EnumerateArray())
                    {
                        if (element.TryGetProperty("skill", out JsonElement skill) &&
                            skill.TryGetProperty("name", out JsonElement name))
                        {
                            skills.Add(name.GetString() ?? string.Empty);
                        }
                    }
                }
            }
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON Parsing Error: {ex.Message}");
        }
        return skills;
    }
}
