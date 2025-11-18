using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerCompassWebApi.Migrations
{
    /// <inheritdoc />
    public partial class CareerRecommendationsSchemaUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CareerCourseworks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CareerId = table.Column<int>(type: "int", nullable: false),
                    CourseworkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerCourseworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CareerCourseworks_Careers_CareerId",
                        column: x => x.CareerId,
                        principalTable: "Careers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CareerCourseworks_Courseworks_CourseworkId",
                        column: x => x.CourseworkId,
                        principalTable: "Courseworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CareerFieldsOfStudy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CareerId = table.Column<int>(type: "int", nullable: false),
                    FieldOfStudyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerFieldsOfStudy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CareerFieldsOfStudy_Careers_CareerId",
                        column: x => x.CareerId,
                        principalTable: "Careers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CareerFieldsOfStudy_FieldsOfStudy_FieldOfStudyId",
                        column: x => x.FieldOfStudyId,
                        principalTable: "FieldsOfStudy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseworkSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseworkId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseworkSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseworkSkills_Courseworks_CourseworkId",
                        column: x => x.CourseworkId,
                        principalTable: "Courseworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseworkSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectSkills_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CareerCourseworks_CareerId",
                table: "CareerCourseworks",
                column: "CareerId");

            migrationBuilder.CreateIndex(
                name: "IX_CareerCourseworks_CourseworkId",
                table: "CareerCourseworks",
                column: "CourseworkId");

            migrationBuilder.CreateIndex(
                name: "IX_CareerFieldsOfStudy_CareerId",
                table: "CareerFieldsOfStudy",
                column: "CareerId");

            migrationBuilder.CreateIndex(
                name: "IX_CareerFieldsOfStudy_FieldOfStudyId",
                table: "CareerFieldsOfStudy",
                column: "FieldOfStudyId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseworkSkills_CourseworkId",
                table: "CourseworkSkills",
                column: "CourseworkId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseworkSkills_SkillId",
                table: "CourseworkSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSkills_ProjectId",
                table: "ProjectSkills",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSkills_SkillId",
                table: "ProjectSkills",
                column: "SkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CareerCourseworks");

            migrationBuilder.DropTable(
                name: "CareerFieldsOfStudy");

            migrationBuilder.DropTable(
                name: "CourseworkSkills");

            migrationBuilder.DropTable(
                name: "ProjectSkills");
        }
    }
}
