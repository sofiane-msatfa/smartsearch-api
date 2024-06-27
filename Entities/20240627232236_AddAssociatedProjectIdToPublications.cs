using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartsearchApi.Entities
{
    /// <inheritdoc />
    public partial class AddAssociatedProjectIdToPublications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Publications",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Publications",
                newName: "Titre");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "Publications",
                newName: "Resume");

            migrationBuilder.RenameColumn(
                name: "PublicationDate",
                table: "Publications",
                newName: "DateDePublication");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Publications",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ProjectId1",
                table: "Publications",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Publications_ProjectId1",
                table: "Publications",
                column: "ProjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_Projects_ProjectId1",
                table: "Publications",
                column: "ProjectId1",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_Projects_ProjectId1",
                table: "Publications");

            migrationBuilder.DropIndex(
                name: "IX_Publications_ProjectId1",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "Publications");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Publications",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Titre",
                table: "Publications",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Resume",
                table: "Publications",
                newName: "Summary");

            migrationBuilder.RenameColumn(
                name: "DateDePublication",
                table: "Publications",
                newName: "PublicationDate");
        }
    }
}
