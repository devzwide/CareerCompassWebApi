using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WebAPI.Core.Entities;
using WebAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebAPI.Presentation.Presentation;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ConnectController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly ApplicationDbContext _db;

    public ConnectController(IConfiguration config, ApplicationDbContext db)
    {
        _config = config;
        _db = db;
    }

    [HttpGet("github")]
    public IActionResult ConnectGitHub()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var clientId = _config["GitHub:ClientId"];
        var redirectUri = _config["GitHub:RedirectUri"];
        var url = $"https://github.com/login/oauth/authorize?client_id={clientId}&redirect_uri={redirectUri}&scope=repo%20read:user";
        return Redirect(url);
    }

    [HttpGet("github/callback")]
    public async Task<IActionResult> GitHubCallback([FromQuery] string code)
    {
        // var userId = state;
        var clientId = _config["GitHub:ClientId"];
        var clientSecret = _config["GitHub:ClientSecret"];
        var redirectUri = _config["GitHub:RedirectUri"];
        using var httpClient = new HttpClient();
        var tokenRequest = new HttpRequestMessage(HttpMethod.Post, "https://github.com/login/oauth/access_token")
        {
            Content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", clientId ?? string.Empty),
                new KeyValuePair<string, string>("client_secret", clientSecret ?? string.Empty),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", redirectUri ?? string.Empty)
            })
        };
        tokenRequest.Headers.Add("Accept", "application/json");
        var response = await httpClient.SendAsync(tokenRequest);
        var json = await response.Content.ReadAsStringAsync();
        var tokenObj = System.Text.Json.JsonDocument.Parse(json).RootElement;
        var accessToken = tokenObj.GetProperty("access_token").GetString();
        if (string.IsNullOrEmpty(accessToken))
            return BadRequest(new { message = "Failed to retrieve GitHub access token." });
        // Save accessToken to authenticated user
        var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
        var user = await _db.Users.FindAsync(userId);
        if (user == null) return Unauthorized();
        user.GitHubAccessToken = accessToken;
        await _db.SaveChangesAsync();
        return Ok(new { message = "GitHub account connected successfully." });
    }

    [HttpGet("linkedin")]
    public IActionResult ConnectLinkedIn()
    {
        var clientId = _config["LinkedIn:ClientId"];
        var redirectUri = _config["LinkedIn:RedirectUri"];
        var url = $"https://www.linkedin.com/oauth/v2/authorization?response_type=code&client_id={clientId}&redirect_uri={redirectUri}&scope=r_liteprofile%20r_emailaddress%20r_skill";
        return Redirect(url);
    }

    [HttpGet("linkedin/callback")]
    public async Task<IActionResult> LinkedInCallback([FromQuery] string code)
    {
        var clientId = _config["LinkedIn:ClientId"];
        var clientSecret = _config["LinkedIn:ClientSecret"];
        var redirectUri = _config["LinkedIn:RedirectUri"];
        using var httpClient = new HttpClient();
        var tokenRequest = new HttpRequestMessage(HttpMethod.Post, "https://www.linkedin.com/oauth/v2/accessToken")
        {
            Content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", redirectUri ?? string.Empty),
                new KeyValuePair<string, string>("client_id", clientId ?? string.Empty),
                new KeyValuePair<string, string>("client_secret", clientSecret ?? string.Empty)
            })
        };
        tokenRequest.Headers.Add("Accept", "application/json");
        var response = await httpClient.SendAsync(tokenRequest);
        var json = await response.Content.ReadAsStringAsync();
        var tokenObj = System.Text.Json.JsonDocument.Parse(json).RootElement;
        var accessToken = tokenObj.GetProperty("access_token").GetString();
        var refreshToken = tokenObj.TryGetProperty("refresh_token", out var rt) ? rt.GetString() : null;
        if (string.IsNullOrEmpty(accessToken))
            return BadRequest(new { message = "Failed to retrieve LinkedIn access token." });
        // Save accessToken and refreshToken to authenticated user
        var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");
        var user = await _db.Users.FindAsync(userId);
        if (user == null) return Unauthorized();
        user.LinkedInAccessToken = accessToken;
        user.LinkedInRefreshToken = refreshToken;
        await _db.SaveChangesAsync();
        return Ok(new { message = "LinkedIn account connected successfully." });
    }
}
