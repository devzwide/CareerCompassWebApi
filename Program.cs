using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Application.Interfaces;
using WebAPI.Application.Services;
using WebAPI.Core.Interfaces;
using WebAPI.Infrastructure.Data;
using WebAPI.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


/* 
    Authentication Dependancy Injection (DI) Services
    Authentication Middleware Configuration
*/
builder.Services.AddScoped<IAuthService, AuthService>(); 
builder.Services.AddScoped<ITokenService, TokenService>();

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

builder.Services.AddScoped<WebAPI.Application.Interfaces.ICareerService, WebAPI.Application.Services.CareerService>();
builder.Services.AddScoped<WebAPI.Application.Interfaces.IRoadmapService, WebAPI.Application.Services.RoadmapService>();
builder.Services.AddScoped<WebAPI.Application.Interfaces.ISkillGapService, WebAPI.Application.Services.SkillGapService>();

// Register the new API client services
builder.Services.AddScoped<GitHubService>();
builder.Services.AddScoped<LinkedInService>();

// Register HttpClient ONLY for LinkedIn.
// GitHubService uses the Octokit client library directly, so it doesn't need a factory registration.
builder.Services.AddHttpClient("LinkedIn", client =>
{
    client.BaseAddress = new Uri("https://api.linkedin.com/v2/");
});
builder.Services.AddScoped<WebAPI.Application.Services.GitHubService>();
builder.Services.AddScoped<WebAPI.Application.Services.LinkedInService>();

// Register the background service
builder.Services.AddHostedService<ProfileSyncService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<WebAPI.Infrastructure.Data.ApplicationDbContext>();
        
        // Run the seeder
        WebAPI.Infrastructure.Data.DataSeeder.Seed(dbContext);
    }
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();