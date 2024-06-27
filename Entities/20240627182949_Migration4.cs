#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartsearchApi.Entities;

/// <inheritdoc />
public partial class Migration4 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Projects",
            table => new
            {
                Id = table.Column<long>("INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Title = table.Column<string>("TEXT", nullable: false),
                Description = table.Column<string>("TEXT", nullable: false),
                StartDate = table.Column<DateTime>("TEXT", nullable: false),
                EndDate = table.Column<DateTime>("TEXT", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Projects", x => x.Id); });

        migrationBuilder.CreateTable(
            "Publications",
            table => new
            {
                id = table.Column<long>("INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Title = table.Column<string>("TEXT", nullable: false),
                Summary = table.Column<string>("TEXT", nullable: false),
                PublicationDate = table.Column<DateTime>("TEXT", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Publications", x => x.id); });

        migrationBuilder.CreateTable(
            "Researchers",
            table => new
            {
                Id = table.Column<long>("INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>("TEXT", nullable: false),
                Specialty = table.Column<string>("TEXT", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Researchers", x => x.Id); });

        migrationBuilder.CreateTable(
            "ProjectResearcher",
            table => new
            {
                ProjectsId = table.Column<long>("INTEGER", nullable: false),
                ResearchersId = table.Column<long>("INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProjectResearcher", x => new { x.ProjectsId, x.ResearchersId });
                table.ForeignKey(
                    "FK_ProjectResearcher_Projects_ProjectsId",
                    x => x.ProjectsId,
                    "Projects",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_ProjectResearcher_Researchers_ResearchersId",
                    x => x.ResearchersId,
                    "Researchers",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "ProjectResearchers",
            table => new
            {
                ProjectId = table.Column<long>("INTEGER", nullable: false),
                ResearcherId = table.Column<long>("INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.ForeignKey(
                    "FK_ProjectResearchers_Projects_ProjectId",
                    x => x.ProjectId,
                    "Projects",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_ProjectResearchers_Researchers_ResearcherId",
                    x => x.ResearcherId,
                    "Researchers",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "IX_ProjectResearcher_ResearchersId",
            "ProjectResearcher",
            "ResearchersId");

        migrationBuilder.CreateIndex(
            "IX_ProjectResearchers_ProjectId",
            "ProjectResearchers",
            "ProjectId");

        migrationBuilder.CreateIndex(
            "IX_ProjectResearchers_ResearcherId",
            "ProjectResearchers",
            "ResearcherId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "ProjectResearcher");

        migrationBuilder.DropTable(
            "ProjectResearchers");

        migrationBuilder.DropTable(
            "Publications");

        migrationBuilder.DropTable(
            "Projects");

        migrationBuilder.DropTable(
            "Researchers");
    }
}