using System.Text;
using CareerCompassWebApi.Application.Interfaces;
using CareerCompassWebApi.Application.Services;
using CareerCompassWebApi.Core.Interfaces;
using CareerCompassWebApi.Infrastructure.Data;
using CareerCompassWebApi.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection of Authentication Services
builder.Services.AddScoped<IAuthService, AuthService>(); 
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"] ?? 
                    throw new ArgumentNullException("Jwt:Key", "Jwt:Key is missing from appsettings.json")
                )
            )
        };
    });


// Dependency Injection of Profile Services
builder.Services.AddScoped<IProfileService, ProfileService>();

// Dependency Injection of Career Path Services
builder.Services.AddScoped<ICareerService, CareerService>();
builder.Services.AddScoped<ISkillGapService, SkillGapService>();
builder.Services.AddScoped<IRoadmapService, RoadmapService>();


// For frontend development server to access the API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // <-- Your React app's URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

    
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
} else
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization(); 

app.MapControllers();

app.Run();