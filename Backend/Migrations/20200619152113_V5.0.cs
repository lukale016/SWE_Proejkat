using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekat.Migrations
{
    public partial class V50 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsPicture");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "News",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Song",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordRef = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Song", x => x.id);
                    table.ForeignKey(
                        name: "FK_Song_Record_RecordRef",
                        column: x => x.RecordRef,
                        principalTable: "Record",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Song_RecordRef",
                table: "Song",
                column: "RecordRef");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Song");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "News");

            migrationBuilder.CreateTable(
                name: "NewsPicture",
                columns: table => new
                {
                    NewsRef = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsPicture", x => new { x.NewsRef, x.Image });
                    table.ForeignKey(
                        name: "FK_NewsPicture_News_NewsRef",
                        column: x => x.NewsRef,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }
    }
}
