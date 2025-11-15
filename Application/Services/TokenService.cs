using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CareerCompassWebApi.Core.Entities;
using CareerCompassWebApi.Application.Interfaces;

namespace CareerCompassWebApi.Application.Services;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;
    private readonly string _issuer;

    public TokenService(IConfiguration config)
    {
        var keyString = config["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key", "Jwt:Key is missing from appsettings.json");

        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
        _issuer = config["Jwt:Issuer"] ?? throw new ArgumentNullException("Jwt:Issuer configuration is missing");
    }

    public string CreateToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.GivenName, user.Username)
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds,
            Issuer = _issuer,
            Audience = _issuer
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
