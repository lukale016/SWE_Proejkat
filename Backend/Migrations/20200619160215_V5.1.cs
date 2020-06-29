using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekat.Migrations
{
    public partial class V51 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "News");

            migrationBuilder.AddColumn<string>(
                name: "ImageLarge",
                table: "News",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageSmall",
                table: "News",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageLarge",
                table: "News");

            migrationBuilder.DropColumn(
                name: "ImageSmall",
                table: "News");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
