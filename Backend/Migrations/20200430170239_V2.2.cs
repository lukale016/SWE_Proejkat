using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekat.Migrations
{
    public partial class V22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Caffe",
                table: "Event",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Caffe",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Event_Caffe",
                table: "Event",
                column: "Caffe");

            migrationBuilder.CreateIndex(
                name: "IX_Caffe_Name",
                table: "Caffe",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Caffe_Caffe",
                table: "Event",
                column: "Caffe",
                principalTable: "Caffe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Caffe_Caffe",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Event_Caffe",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Caffe_Name",
                table: "Caffe");

            migrationBuilder.DropColumn(
                name: "Caffe",
                table: "Event");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Caffe",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
