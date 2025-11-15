using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class V2_CareerCompassSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Career_Roadmap_RoadmapId",
                table: "Career");

            migrationBuilder.DropForeignKey(
                name: "FK_CareerSkill_Career_CareerId",
                table: "CareerSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_CareerSkill_Skill_SkillId",
                table: "CareerSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_RoadmapResource_Resource_ResourceId",
                table: "RoadmapResource");

            migrationBuilder.DropForeignKey(
                name: "FK_RoadmapResource_Roadmap_RoadmapId",
                table: "RoadmapResource");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCareer_Career_CareerId",
                table: "UserCareer");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCareer_Users_UserId",
                table: "UserCareer");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCoursework_Coursework_CourseworkId",
                table: "UserCoursework");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCoursework_Users_UserId",
                table: "UserCoursework");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInterest_Interest_InterestId",
                table: "UserInterest");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInterest_Users_UserId",
                table: "UserInterest");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoadmapProgress_RoadmapResource_RoadmapResourceId",
                table: "UserRoadmapProgress");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoadmapProgress_Users_UserId",
                table: "UserRoadmapProgress");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_FieldOfStudy_FieldOfStudyId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_University_UniversityId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkill_Skill_SkillId",
                table: "UserSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkill_Users_UserId",
                table: "UserSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSkill",
                table: "UserSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoadmapProgress",
                table: "UserRoadmapProgress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInterest",
                table: "UserInterest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCoursework",
                table: "UserCoursework");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCareer",
                table: "UserCareer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_University",
                table: "University");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skill",
                table: "Skill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoadmapResource",
                table: "RoadmapResource");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roadmap",
                table: "Roadmap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resource",
                table: "Resource");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interest",
                table: "Interest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FieldOfStudy",
                table: "FieldOfStudy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Coursework",
                table: "Coursework");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CareerSkill",
                table: "CareerSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Career",
                table: "Career");

            migrationBuilder.RenameTable(
                name: "UserSkill",
                newName: "UserSkills");

            migrationBuilder.RenameTable(
                name: "UserRoadmapProgress",
                newName: "UserRoadmapProgresses");

            migrationBuilder.RenameTable(
                name: "UserInterest",
                newName: "UserInterests");

            migrationBuilder.RenameTable(
                name: "UserCoursework",
                newName: "UserCourseworks");

            migrationBuilder.RenameTable(
                name: "UserCareer",
                newName: "UserCareers");

            migrationBuilder.RenameTable(
                name: "University",
                newName: "Universities");

            migrationBuilder.RenameTable(
                name: "Skill",
                newName: "Skills");

            migrationBuilder.RenameTable(
                name: "RoadmapResource",
                newName: "RoadmapResources");

            migrationBuilder.RenameTable(
                name: "Roadmap",
                newName: "Roadmaps");

            migrationBuilder.RenameTable(
                name: "Resource",
                newName: "Resources");

            migrationBuilder.RenameTable(
                name: "Interest",
                newName: "Interests");

            migrationBuilder.RenameTable(
                name: "FieldOfStudy",
                newName: "FieldsOfStudy");

            migrationBuilder.RenameTable(
                name: "Coursework",
                newName: "Courseworks");

            migrationBuilder.RenameTable(
                name: "CareerSkill",
                newName: "CareerSkills");

            migrationBuilder.RenameTable(
                name: "Career",
                newName: "Careers");

            migrationBuilder.RenameIndex(
                name: "IX_UserSkill_UserId",
                table: "UserSkills",
                newName: "IX_UserSkills_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSkill_SkillId",
                table: "UserSkills",
                newName: "IX_UserSkills_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoadmapProgress_UserId",
                table: "UserRoadmapProgresses",
                newName: "IX_UserRoadmapProgresses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoadmapProgress_RoadmapResourceId",
                table: "UserRoadmapProgresses",
                newName: "IX_UserRoadmapProgresses_RoadmapResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_UserInterest_UserId",
                table: "UserInterests",
                newName: "IX_UserInterests_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserInterest_InterestId",
                table: "UserInterests",
                newName: "IX_UserInterests_InterestId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCoursework_UserId",
                table: "UserCourseworks",
                newName: "IX_UserCourseworks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCoursework_CourseworkId",
                table: "UserCourseworks",
                newName: "IX_UserCourseworks_CourseworkId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCareer_UserId",
                table: "UserCareers",
                newName: "IX_UserCareers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCareer_CareerId",
                table: "UserCareers",
                newName: "IX_UserCareers_CareerId");

            migrationBuilder.RenameIndex(
                name: "IX_RoadmapResource_RoadmapId",
                table: "RoadmapResources",
                newName: "IX_RoadmapResources_RoadmapId");

            migrationBuilder.RenameIndex(
                name: "IX_RoadmapResource_ResourceId",
                table: "RoadmapResources",
                newName: "IX_RoadmapResources_ResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_CareerSkill_SkillId",
                table: "CareerSkills",
                newName: "IX_CareerSkills_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_CareerSkill_CareerId",
                table: "CareerSkills",
                newName: "IX_CareerSkills_CareerId");

            migrationBuilder.RenameIndex(
                name: "IX_Career_RoadmapId",
                table: "Careers",
                newName: "IX_Careers_RoadmapId");

            migrationBuilder.AddColumn<string>(
                name: "GitHubAccessToken",
                table: "Users",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedInAccessToken",
                table: "Users",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedInRefreshToken",
                table: "Users",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSkills",
                table: "UserSkills",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoadmapProgresses",
                table: "UserRoadmapProgresses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInterests",
                table: "UserInterests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCourseworks",
                table: "UserCourseworks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCareers",
                table: "UserCareers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Universities",
                table: "Universities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skills",
                table: "Skills",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoadmapResources",
                table: "RoadmapResources",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roadmaps",
                table: "Roadmaps",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resources",
                table: "Resources",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interests",
                table: "Interests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FieldsOfStudy",
                table: "FieldsOfStudy",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courseworks",
                table: "Courseworks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CareerSkills",
                table: "CareerSkills",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Careers",
                table: "Careers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RepositoryUrl = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoadmaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoadmapId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoadmaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoadmaps_Roadmaps_RoadmapId",
                        column: x => x.RoadmapId,
                        principalTable: "Roadmaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoadmaps_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProjects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProjects_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProjects_ProjectId",
                table: "UserProjects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProjects_UserId",
                table: "UserProjects",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoadmaps_RoadmapId",
                table: "UserRoadmaps",
                column: "RoadmapId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoadmaps_UserId",
                table: "UserRoadmaps",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Careers_Roadmaps_RoadmapId",
                table: "Careers",
                column: "RoadmapId",
                principalTable: "Roadmaps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CareerSkills_Careers_CareerId",
                table: "CareerSkills",
                column: "CareerId",
                principalTable: "Careers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CareerSkills_Skills_SkillId",
                table: "CareerSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoadmapResources_Resources_ResourceId",
                table: "RoadmapResources",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoadmapResources_Roadmaps_RoadmapId",
                table: "RoadmapResources",
                column: "RoadmapId",
                principalTable: "Roadmaps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCareers_Careers_CareerId",
                table: "UserCareers",
                column: "CareerId",
                principalTable: "Careers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCareers_Users_UserId",
                table: "UserCareers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourseworks_Courseworks_CourseworkId",
                table: "UserCourseworks",
                column: "CourseworkId",
                principalTable: "Courseworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourseworks_Users_UserId",
                table: "UserCourseworks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterests_Interests_InterestId",
                table: "UserInterests",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterests_Users_UserId",
                table: "UserInterests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoadmapProgresses_RoadmapResources_RoadmapResourceId",
                table: "UserRoadmapProgresses",
                column: "RoadmapResourceId",
                principalTable: "RoadmapResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoadmapProgresses_Users_UserId",
                table: "UserRoadmapProgresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_FieldsOfStudy_FieldOfStudyId",
                table: "Users",
                column: "FieldOfStudyId",
                principalTable: "FieldsOfStudy",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Universities_UniversityId",
                table: "Users",
                column: "UniversityId",
                principalTable: "Universities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkills_Skills_SkillId",
                table: "UserSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkills_Users_UserId",
                table: "UserSkills",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Careers_Roadmaps_RoadmapId",
                table: "Careers");

            migrationBuilder.DropForeignKey(
                name: "FK_CareerSkills_Careers_CareerId",
                table: "CareerSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_CareerSkills_Skills_SkillId",
                table: "CareerSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_RoadmapResources_Resources_ResourceId",
                table: "RoadmapResources");

            migrationBuilder.DropForeignKey(
                name: "FK_RoadmapResources_Roadmaps_RoadmapId",
                table: "RoadmapResources");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCareers_Careers_CareerId",
                table: "UserCareers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCareers_Users_UserId",
                table: "UserCareers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCourseworks_Courseworks_CourseworkId",
                table: "UserCourseworks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCourseworks_Users_UserId",
                table: "UserCourseworks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInterests_Interests_InterestId",
                table: "UserInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInterests_Users_UserId",
                table: "UserInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoadmapProgresses_RoadmapResources_RoadmapResourceId",
                table: "UserRoadmapProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoadmapProgresses_Users_UserId",
                table: "UserRoadmapProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_FieldsOfStudy_FieldOfStudyId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Universities_UniversityId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkills_Skills_SkillId",
                table: "UserSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkills_Users_UserId",
                table: "UserSkills");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "UserProjects");

            migrationBuilder.DropTable(
                name: "UserRoadmaps");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSkills",
                table: "UserSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoadmapProgresses",
                table: "UserRoadmapProgresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInterests",
                table: "UserInterests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCourseworks",
                table: "UserCourseworks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCareers",
                table: "UserCareers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Universities",
                table: "Universities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skills",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roadmaps",
                table: "Roadmaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoadmapResources",
                table: "RoadmapResources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resources",
                table: "Resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interests",
                table: "Interests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FieldsOfStudy",
                table: "FieldsOfStudy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courseworks",
                table: "Courseworks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CareerSkills",
                table: "CareerSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Careers",
                table: "Careers");

            migrationBuilder.DropColumn(
                name: "GitHubAccessToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LinkedInAccessToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LinkedInRefreshToken",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "UserSkills",
                newName: "UserSkill");

            migrationBuilder.RenameTable(
                name: "UserRoadmapProgresses",
                newName: "UserRoadmapProgress");

            migrationBuilder.RenameTable(
                name: "UserInterests",
                newName: "UserInterest");

            migrationBuilder.RenameTable(
                name: "UserCourseworks",
                newName: "UserCoursework");

            migrationBuilder.RenameTable(
                name: "UserCareers",
                newName: "UserCareer");

            migrationBuilder.RenameTable(
                name: "Universities",
                newName: "University");

            migrationBuilder.RenameTable(
                name: "Skills",
                newName: "Skill");

            migrationBuilder.RenameTable(
                name: "Roadmaps",
                newName: "Roadmap");

            migrationBuilder.RenameTable(
                name: "RoadmapResources",
                newName: "RoadmapResource");

            migrationBuilder.RenameTable(
                name: "Resources",
                newName: "Resource");

            migrationBuilder.RenameTable(
                name: "Interests",
                newName: "Interest");

            migrationBuilder.RenameTable(
                name: "FieldsOfStudy",
                newName: "FieldOfStudy");

            migrationBuilder.RenameTable(
                name: "Courseworks",
                newName: "Coursework");

            migrationBuilder.RenameTable(
                name: "CareerSkills",
                newName: "CareerSkill");

            migrationBuilder.RenameTable(
                name: "Careers",
                newName: "Career");

            migrationBuilder.RenameIndex(
                name: "IX_UserSkills_UserId",
                table: "UserSkill",
                newName: "IX_UserSkill_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSkills_SkillId",
                table: "UserSkill",
                newName: "IX_UserSkill_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoadmapProgresses_UserId",
                table: "UserRoadmapProgress",
                newName: "IX_UserRoadmapProgress_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoadmapProgresses_RoadmapResourceId",
                table: "UserRoadmapProgress",
                newName: "IX_UserRoadmapProgress_RoadmapResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_UserInterests_UserId",
                table: "UserInterest",
                newName: "IX_UserInterest_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserInterests_InterestId",
                table: "UserInterest",
                newName: "IX_UserInterest_InterestId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCourseworks_UserId",
                table: "UserCoursework",
                newName: "IX_UserCoursework_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCourseworks_CourseworkId",
                table: "UserCoursework",
                newName: "IX_UserCoursework_CourseworkId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCareers_UserId",
                table: "UserCareer",
                newName: "IX_UserCareer_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCareers_CareerId",
                table: "UserCareer",
                newName: "IX_UserCareer_CareerId");

            migrationBuilder.RenameIndex(
                name: "IX_RoadmapResources_RoadmapId",
                table: "RoadmapResource",
                newName: "IX_RoadmapResource_RoadmapId");

            migrationBuilder.RenameIndex(
                name: "IX_RoadmapResources_ResourceId",
                table: "RoadmapResource",
                newName: "IX_RoadmapResource_ResourceId");

            migrationBuilder.RenameIndex(
                name: "IX_CareerSkills_SkillId",
                table: "CareerSkill",
                newName: "IX_CareerSkill_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_CareerSkills_CareerId",
                table: "CareerSkill",
                newName: "IX_CareerSkill_CareerId");

            migrationBuilder.RenameIndex(
                name: "IX_Careers_RoadmapId",
                table: "Career",
                newName: "IX_Career_RoadmapId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSkill",
                table: "UserSkill",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoadmapProgress",
                table: "UserRoadmapProgress",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInterest",
                table: "UserInterest",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCoursework",
                table: "UserCoursework",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCareer",
                table: "UserCareer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_University",
                table: "University",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skill",
                table: "Skill",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roadmap",
                table: "Roadmap",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoadmapResource",
                table: "RoadmapResource",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resource",
                table: "Resource",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interest",
                table: "Interest",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FieldOfStudy",
                table: "FieldOfStudy",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coursework",
                table: "Coursework",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CareerSkill",
                table: "CareerSkill",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Career",
                table: "Career",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Career_Roadmap_RoadmapId",
                table: "Career",
                column: "RoadmapId",
                principalTable: "Roadmap",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CareerSkill_Career_CareerId",
                table: "CareerSkill",
                column: "CareerId",
                principalTable: "Career",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CareerSkill_Skill_SkillId",
                table: "CareerSkill",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoadmapResource_Resource_ResourceId",
                table: "RoadmapResource",
                column: "ResourceId",
                principalTable: "Resource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoadmapResource_Roadmap_RoadmapId",
                table: "RoadmapResource",
                column: "RoadmapId",
                principalTable: "Roadmap",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCareer_Career_CareerId",
                table: "UserCareer",
                column: "CareerId",
                principalTable: "Career",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCareer_Users_UserId",
                table: "UserCareer",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCoursework_Coursework_CourseworkId",
                table: "UserCoursework",
                column: "CourseworkId",
                principalTable: "Coursework",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCoursework_Users_UserId",
                table: "UserCoursework",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterest_Interest_InterestId",
                table: "UserInterest",
                column: "InterestId",
                principalTable: "Interest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterest_Users_UserId",
                table: "UserInterest",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoadmapProgress_RoadmapResource_RoadmapResourceId",
                table: "UserRoadmapProgress",
                column: "RoadmapResourceId",
                principalTable: "RoadmapResource",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoadmapProgress_Users_UserId",
                table: "UserRoadmapProgress",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_FieldOfStudy_FieldOfStudyId",
                table: "Users",
                column: "FieldOfStudyId",
                principalTable: "FieldOfStudy",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_University_UniversityId",
                table: "Users",
                column: "UniversityId",
                principalTable: "University",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkill_Skill_SkillId",
                table: "UserSkill",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkill_Users_UserId",
                table: "UserSkill",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
