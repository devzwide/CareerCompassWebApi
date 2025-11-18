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


builder.Services.AddScoped<IProfileService, ProfileService>();

builder.Services.AddScoped<ICareerService, CareerService>();
builder.Services.AddScoped<ISkillGapService, SkillGapService>();
builder.Services.AddScoped<IRoadmapService, RoadmapService>();

builder.Services.AddScoped<IProfileService, ProfileService>();

// Add these services for the Proof of Work Tracker
builder.Services.AddHttpClient(); // Required for LinkedInService
builder.Services.AddHttpClient("LinkedIn", client =>
{
    client.BaseAddress = new Uri("https://api.linkedin.com/");
});
builder.Services.AddSingleton<GithubService>(); // Singleton is fine as Octokit client is thread-safe
builder.Services.AddScoped<LinkedInService>(); 
builder.Services.AddScoped<IProfileEnrichmentService, ProfileEnrichmentService>();

builder.Services.AddScoped<ICareerService, CareerService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

    
var app = builder.Build();

{ 
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        
        await Seed.SeedDataAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during database seeding.");
    }
}

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