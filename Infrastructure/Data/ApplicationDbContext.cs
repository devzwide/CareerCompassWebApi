using System;
using Microsoft.EntityFrameworkCore;
using CareerCompassWebApi.Core.Entities;

namespace CareerCompassWebApi.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserCareer> UserCareers { get; set; }
    public DbSet<UserCoursework> UserCourseworks { get; set; }
    public DbSet<UserInterest> UserInterests { get; set; }
    public DbSet<UserProject> UserProjects { get; set; }
    public DbSet<UserRoadmap> UserRoadmaps { get; set; }
    public DbSet<UserRoadmapProgress> UserRoadmapProgresses { get; set; }
    public DbSet<UserSkill> UserSkills { get; set; }

    public DbSet<FieldOfStudy> FieldsOfStudy { get; set; }
    public DbSet<University> Universities { get; set; }

    public DbSet<Career> Careers { get; set; }
    public DbSet<CareerSkill> CareerSkills { get; set; }
    public DbSet<JobMarketSkill> JobMarketSkills { get; set; }


    public DbSet<Interest> Interests { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Roadmap> Roadmaps { get; set; }
    public DbSet<RoadmapResource> RoadmapResources { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Skill> Skills { get; set; }

}
