using CareerCompassWebApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CareerCompassWebApi.Infrastructure.Data
{
    public static class Seed
    {
        public static async Task SeedDataAsync(ApplicationDbContext context)
        {
            await context.Database.MigrateAsync();

            await SeedSkillsAsync(context);
            await SeedInterestsAsync(context);
            await SeedFieldsOfStudyAsync(context);
            await SeedResourcesAsync(context);
            await SeedRoadmapsAsync(context);

            await SeedCareersAsync(context); 

            await SeedCareerSkillsAsync(context); 
            await SeedRoadmapResourcesAsync(context);
        }

        private static async Task SeedSkillsAsync(ApplicationDbContext context)
        {
            if (await context.Skills.AnyAsync()) return;

            var skills = new List<Skill>
            {
                new Skill { Name = "ASP.NET Core", Type = SkillType.Framework },
                new Skill { Name = "React", Type = SkillType.Framework },
                new Skill { Name = "SQL Server", Type = SkillType.Tool },
                new Skill { Name = "Linux", Type = SkillType.Tool },
                new Skill { Name = "Power BI", Type = SkillType.Tool },
                new Skill { Name = "C#", Type = SkillType.Language },
                new Skill { Name = "JavaScript", Type = SkillType.Language },
                new Skill { Name = "Data Analysis", Type = SkillType.Other },
                new Skill { Name = "Agile Methodologies", Type = SkillType.Other },
                new Skill { Name = "Problem Solving", Type = SkillType.Other }
            };

            await context.Skills.AddRangeAsync(skills);
            await context.SaveChangesAsync();
        }

        private static async Task SeedRoadmapsAsync(ApplicationDbContext context)
        {
            if (await context.Roadmaps.AnyAsync()) return;

            var roadmaps = new List<Roadmap>
            {
                new Roadmap 
                { 
                    Title = "Junior ASP.NET Developer Roadmap", 
                    Type = RoadmapType.TechnicalSkill 
                },
                new Roadmap 
                { 
                    Title = "Junior Power BI Analyst Roadmap", 
                    Type = RoadmapType.TechnicalSkill 
                }
            };

            await context.Roadmaps.AddRangeAsync(roadmaps);
            await context.SaveChangesAsync();
        }

        private static async Task SeedCareersAsync(ApplicationDbContext context)
        {
            if (await context.Careers.AnyAsync()) return;

            var defaultRoadmap = await context.Roadmaps.FirstOrDefaultAsync(r => 
                r.Title == "Junior ASP.NET Developer Roadmap");
            
            if (defaultRoadmap == null) return; 

            var careers = new List<Career>
            {
                new Career 
                { 
                    Title = "Software Engineer", 
                    AverageSalary = 95000, 
                    MarketDemandScore = 8, 
                    SalaryPotentialScore = 9,
                    RoadmapId = defaultRoadmap.Id 
                },
                new Career 
                { 
                    Title = "Business Intelligence Analyst", 
                    AverageSalary = 85000, 
                    MarketDemandScore = 7, 
                    SalaryPotentialScore = 8,
                    RoadmapId = defaultRoadmap.Id 
                }
            };

            await context.Careers.AddRangeAsync(careers);
            await context.SaveChangesAsync();
        }

        private static async Task SeedResourcesAsync(ApplicationDbContext context)
        {
            if (await context.Resources.AnyAsync()) return;

            var resources = new List<Resource>
            {
                new Resource 
                { 
                    Name = "Microsoft Learn: ASP.NET Core", 
                    ResourceUrl = "https://learn.microsoft.com/en-us/aspnet/core/",
                    ResourceType = ResourceType.TechnicalSkill 
                },
                new Resource 
                { 
                    Name = "React Official Docs",
                    ResourceUrl = "https://react.dev/",
                    ResourceType = ResourceType.TechnicalSkill
                },
                new Resource 
                { 
                    Name = "SQL Server Documentation",
                    ResourceUrl = "https://learn.microsoft.com/en-us/sql/sql-server/",
                    ResourceType = ResourceType.TechnicalSkill
                },
                new Resource 
                { 
                    Name = "Power BI Documentation",
                    ResourceUrl = "https://learn.microsoft.com/en-us/power-bi/",
                    ResourceType = ResourceType.TechnicalSkill
                }
            };

            await context.Resources.AddRangeAsync(resources);
            await context.SaveChangesAsync();
        }

        private static async Task SeedInterestsAsync(ApplicationDbContext context)
        {
            if (await context.Interests.AnyAsync()) return;
            var interests = new List<Interest>
            {
                new Interest { Name = "Web Development" },
                new Interest { Name = "Data Visualization" },
                new Interest { Name = "Artificial Intelligence" }
            };
            await context.Interests.AddRangeAsync(interests);
            await context.SaveChangesAsync();
        }
        
        private static async Task SeedFieldsOfStudyAsync(ApplicationDbContext context)
        {
            if (await context.FieldsOfStudy.AnyAsync()) return;
            var fields = new List<FieldOfStudy>
            {
                new FieldOfStudy { Name = "Computer Science" },
                new FieldOfStudy { Name = "Information Technology" }
            };
            await context.FieldsOfStudy.AddRangeAsync(fields);
            await context.SaveChangesAsync();
        }
        
        private static async Task SeedCareerSkillsAsync(ApplicationDbContext context)
        {
            if (await context.CareerSkills.AnyAsync()) return;

            var seCareer = await context.Careers.FirstOrDefaultAsync(c => c.Title == "Software Engineer");
            var biCareer = await context.Careers.FirstOrDefaultAsync(c => c.Title == "Business Intelligence Analyst");
            var aspNetSkill = await context.Skills.FirstOrDefaultAsync(s => s.Name == "ASP.NET Core");
            var sqlSkill = await context.Skills.FirstOrDefaultAsync(s => s.Name == "SQL Server");
            var powerBiSkill = await context.Skills.FirstOrDefaultAsync(s => s.Name == "Power BI");

            if (seCareer == null || biCareer == null || aspNetSkill == null || sqlSkill == null || powerBiSkill == null)
            {
                return; 
            }

            var careerSkills = new List<CareerSkill>
            {
                new CareerSkill { Career = seCareer, Skill = aspNetSkill }, 
                new CareerSkill { Career = seCareer, Skill = sqlSkill },
                new CareerSkill { Career = biCareer, Skill = powerBiSkill },
                new CareerSkill { Career = biCareer, Skill = sqlSkill }
            };

            await context.CareerSkills.AddRangeAsync(careerSkills);
            await context.SaveChangesAsync();
        }
        
        private static async Task SeedRoadmapResourcesAsync(ApplicationDbContext context)
        {
            if (await context.RoadmapResources.AnyAsync()) return;

            var aspNetRoadmap = await context.Roadmaps.FirstOrDefaultAsync(r => r.Title == "Junior ASP.NET Developer Roadmap");
            var biRoadmap = await context.Roadmaps.FirstOrDefaultAsync(r => r.Title == "Junior Power BI Analyst Roadmap");
            var aspNetRes = await context.Resources.FirstOrDefaultAsync(r => r.Name == "Microsoft Learn: ASP.NET Core");
            var sqlRes = await context.Resources.FirstOrDefaultAsync(r => r.Name == "SQL Server Documentation");
            var powerBiRes = await context.Resources.FirstOrDefaultAsync(r => r.Name == "Power BI Documentation");

            if (aspNetRoadmap == null || biRoadmap == null || aspNetRes == null || sqlRes == null || powerBiRes == null)
            {
                return; 
            }

            var roadmapResources = new List<RoadmapResource>
            {
                new RoadmapResource { Roadmap = aspNetRoadmap, Resource = aspNetRes, StepOrder = 1, StepTitle = "Learn ASP.NET Core Basics" },
                new RoadmapResource { Roadmap = aspNetRoadmap, Resource = sqlRes, StepOrder = 2, StepTitle = "Learn SQL Server" },
                
                new RoadmapResource { Roadmap = biRoadmap, Resource = powerBiRes, StepOrder = 1, StepTitle = "Learn Power BI Basics" },
                new RoadmapResource { Roadmap = biRoadmap, Resource = sqlRes, StepOrder = 2, StepTitle = "Learn SQL for BI" }
            };

            await context.RoadmapResources.AddRangeAsync(roadmapResources);
            await context.SaveChangesAsync();
        }
    }
}