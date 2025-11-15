using Microsoft.EntityFrameworkCore;
using WebAPI.Core.Entities;
using System.Linq;
using System;

namespace WebAPI.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Run migrations if they are not applied
            context.Database.Migrate();

            // Check if data already exists. If so, don't seed.
            if (context.Careers.Any())
            {
                return; // Database has been seeded
            }

            // ---
            // This is the power of a seeder class. We can use C# to
            // create objects and let EF Core's Change Tracker handle the
            // foreign keys. Notice we don't need to manage IDs at all.
            // ---

            // 1. Skills
            var skillAspNet = new Skill { Name = "ASP.NET Core", Description = "Build web APIs", Type = SkillType.Framework, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            var skillReact = new Skill { Name = "React", Description = "Build user interfaces", Type = SkillType.Framework, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            var skillSql = new Skill { Name = "SQL Server", Description = "Manage data", Type = SkillType.Tool, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            var skillPowerBi = new Skill { Name = "Power BI", Description = "Visualize data", Type = SkillType.Tool, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            context.Skills.AddRange(skillAspNet, skillReact, skillSql, skillPowerBi);

            // 2. Interests
            var intWeb = new Interest { Name = "Web Development" };
            var intBi = new Interest { Name = "Business Intelligence" };
            context.Interests.AddRange(intWeb, intBi);

            // 3. Roadmaps
            var roadmapFullStack = new Roadmap { Title = "Full Stack Roadmap", Description = "The path to becoming a Full Stack Developer.", Type = RoadmapType.Other, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            var roadmapBi = new Roadmap { Title = "BI Analyst Roadmap", Description = "The path to becoming a BI Analyst.", Type = RoadmapType.Other, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            // We don't need to 'Add' these yet; 'Career' will do it.

            // 4. Careers (and link them to their roadmaps)
            var careerFullStack = new Career { 
                Title = "Full Stack Developer", 
                Description = "Build end-to-end web applications.", 
                AverageSalary = 95000, 
                MarketDemandScore = 0.9, 
                SalaryPotentialScore = 9, 
                Roadmap = roadmapFullStack, // <-- EF Core links this
                CreatedAt = DateTime.UtcNow, 
                UpdatedAt = DateTime.UtcNow 
            };
            var careerBi = new Career { 
                Title = "BI Analyst", 
                Description = "Turn data into actionable insights.", 
                AverageSalary = 80000, 
                MarketDemandScore = 0.8, 
                SalaryPotentialScore = 8, 
                Roadmap = roadmapBi, // <-- EF Core links this
                CreatedAt = DateTime.UtcNow, 
                UpdatedAt = DateTime.UtcNow 
            };
            context.Careers.AddRange(careerFullStack, careerBi);

            // 5. Link Careers to their required Skills
            context.CareerSkills.AddRange(
                new CareerSkill { Career = careerFullStack, Skill = skillAspNet },
                new CareerSkill { Career = careerFullStack, Skill = skillReact },
                new CareerSkill { Career = careerBi, Skill = skillSql },
                new CareerSkill { Career = careerBi, Skill = skillPowerBi }
            );

            // 6. Resources
            var resReact = new Resource { Name = "Official React Docs", ResourceUrl = "https://react.dev/", ResourceType = ResourceType.TechnicalSkill, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            var resAspNet = new Resource { Name = "Official ASP.NET Core Docs", ResourceUrl = "https://learn.microsoft.com/en-us/aspnet/core/", ResourceType = ResourceType.TechnicalSkill, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            var resPowerBi = new Resource { Name = "Microsoft Power BI Docs", ResourceUrl = "https://learn.microsoft.com/en-us/power-bi/", ResourceType = ResourceType.TechnicalSkill, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            var resSql = new Resource { Name = "SQL Server Docs", ResourceUrl = "https://learn.microsoft.com/en-us/sql/sql-server/", ResourceType = ResourceType.TechnicalSkill, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow };
            context.Resources.AddRange(resReact, resAspNet, resPowerBi, resSql);

            // 7. Link Roadmaps to Resources
            context.RoadmapResources.AddRange(
                new RoadmapResource { Roadmap = roadmapFullStack, Resource = resAspNet, StepOrder = 1, StepTitle = "Learn ASP.NET Core", StepDescription = "Build the backend API." },
                new RoadmapResource { Roadmap = roadmapFullStack, Resource = resReact, StepOrder = 2, StepTitle = "Learn React", StepDescription = "Build the frontend client." },
                new RoadmapResource { Roadmap = roadmapBi, Resource = resSql, StepOrder = 1, StepTitle = "Learn SQL", StepDescription = "Master data querying." },
                new RoadmapResource { Roadmap = roadmapBi, Resource = resPowerBi, StepOrder = 2, StepTitle = "Learn Power BI", StepDescription = "Master data visualization." }
            );

            // 8. Seed Job Market Skill (Simulated ETL Results)
            context.JobMarketSkills.AddRange(
                // Full Stack Developer Market Skills:
                // Our roadmap requires ASP.NET and React, but the market ALSO wants SQL.
                new JobMarketSkill { Career = careerFullStack, Skill = skillAspNet, Frequency = 0.8 }, // 80% demand
                new JobMarketSkill { Career = careerFullStack, Skill = skillReact, Frequency = 0.9 }, // 90% demand
                new JobMarketSkill { Career = careerFullStack, Skill = skillSql, Frequency = 0.7 }, // 70% demand (The "Gap")

                // BI Analyst Market Skills:
                // Our roadmap requires SQL and PowerBI.
                new JobMarketSkill { Career = careerBi, Skill = skillSql, Frequency = 1.0 }, // 100% demand
                new JobMarketSkill { Career = careerBi, Skill = skillPowerBi, Frequency = 0.9 } // 90% demand
            );

            // 9. Save all changes to the database
            context.SaveChanges();
        }
    }
}