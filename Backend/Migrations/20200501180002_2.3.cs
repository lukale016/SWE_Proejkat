using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekat.Migrations
{
    public partial class _23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Caffe",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Caffe",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Caffe");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Caffe");
        }
    }
}
