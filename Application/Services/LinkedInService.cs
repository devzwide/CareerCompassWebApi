using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI.Application.Services
{
    public class LinkedInService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LinkedInService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Fetches the skills for the user authenticated with the given access token.
        /// </summary>
        /// <param name="accessToken">The user's LinkedIn OAuth access token.</param>
        /// <returns>A list of skill names as strings.</returns>
        public async Task<List<string>> GetSkillsAsync(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                return new List<string>();
            }

            // Create a named HttpClient from the factory.
            var client = _httpClientFactory.CreateClient("LinkedIn");

            // Add the OAuth 2.0 access token to the request header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            // Note: 'r_skill' scope is complex. We'll use a simplified endpoint
            // that projections can pull skills from. This may need adjustment
            // based on your exact LinkedIn App permissions.
            var url = "https://api.linkedin.com/v2/me?projection=(skills)";

            try
            {
                var response = await client.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                {
                    // Log the error
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
}