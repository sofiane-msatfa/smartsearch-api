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

            migrationBuilder.AddColumn<long>(
                name: "AssociatedProjectId",
                table: "Publications",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssociatedProjectId",
                table: "Publications");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Publications",
                newName: "id");
        }
    }
}
