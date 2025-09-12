using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourismManager.Migrations
{
    /// <inheritdoc />
    public partial class day : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Packages");

            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "Packages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "Packages");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Packages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
