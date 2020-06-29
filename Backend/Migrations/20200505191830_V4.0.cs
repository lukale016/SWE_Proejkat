using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekat.Migrations
{
    public partial class V40 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Record_BoundRecordRef",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_OwnerRef",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Caffe_Caffe",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecord_User_UserRef",
                table: "FavoriteRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_Friend_User_User1Ref",
                table: "Friend");

            migrationBuilder.DropForeignKey(
                name: "FK_Interested_User_AttenderRef",
                table: "Interested");

            migrationBuilder.DropForeignKey(
                name: "FK_Interested_Event_EventRef",
                table: "Interested");

            migrationBuilder.DropForeignKey(
                name: "FK_PendingRequest_User_UserReceavedRef",
                table: "PendingRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_PendingRequest_User_UserSentRef",
                table: "PendingRequest");

            migrationBuilder.DropColumn(
                name: "BackgroundImg",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Info",
                table: "News");

            migrationBuilder.DropColumn(
                name: "OverlayText",
                table: "News");

            migrationBuilder.DropColumn(
                name: "DateAndTime",
                table: "Event");

            migrationBuilder.AddColumn<string>(
                name: "TextLarge",
                table: "News",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextSmall",
                table: "News",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleLarge",
                table: "News",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleSmall",
                table: "News",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Event",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "Event",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NewsPicture",
                columns: table => new
                {
                    NewsRef = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    NewsId = table.Column<int>(nullable: true)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Record_BoundRecordRef",
                table: "Comment",
                column: "BoundRecordRef",
                principalTable: "Record",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_OwnerRef",
                table: "Comment",
                column: "OwnerRef",
                principalTable: "User",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Caffe_Caffe",
                table: "Event",
                column: "Caffe",
                principalTable: "Caffe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecord_User_UserRef",
                table: "FavoriteRecord",
                column: "UserRef",
                principalTable: "User",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_User_User1Ref",
                table: "Friend",
                column: "User1Ref",
                principalTable: "User",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interested_User_AttenderRef",
                table: "Interested",
                column: "AttenderRef",
                principalTable: "User",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Interested_Event_EventRef",
                table: "Interested",
                column: "EventRef",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PendingRequest_User_UserReceavedRef",
                table: "PendingRequest",
                column: "UserReceavedRef",
                principalTable: "User",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PendingRequest_User_UserSentRef",
                table: "PendingRequest",
                column: "UserSentRef",
                principalTable: "User",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Record_BoundRecordRef",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_OwnerRef",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Caffe_Caffe",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRecord_User_UserRef",
                table: "FavoriteRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_Friend_User_User1Ref",
                table: "Friend");

            migrationBuilder.DropForeignKey(
                name: "FK_Interested_User_AttenderRef",
                table: "Interested");

            migrationBuilder.DropForeignKey(
                name: "FK_Interested_Event_EventRef",
                table: "Interested");

            migrationBuilder.DropForeignKey(
                name: "FK_PendingRequest_User_UserReceavedRef",
                table: "PendingRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_PendingRequest_User_UserSentRef",
                table: "PendingRequest");

            migrationBuilder.DropTable(
                name: "NewsPicture");

            migrationBuilder.DropColumn(
                name: "TextLarge",
                table: "News");

            migrationBuilder.DropColumn(
                name: "TextSmall",
                table: "News");

            migrationBuilder.DropColumn(
                name: "TitleLarge",
                table: "News");

            migrationBuilder.DropColumn(
                name: "TitleSmall",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Event");

            migrationBuilder.AddColumn<string>(
                name: "BackgroundImg",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Info",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OverlayText",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateAndTime",
                table: "Event",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Record_BoundRecordRef",
                table: "Comment",
                column: "BoundRecordRef",
                principalTable: "Record",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_OwnerRef",
                table: "Comment",
                column: "OwnerRef",
                principalTable: "User",
                principalColumn: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Caffe_Caffe",
                table: "Event",
                column: "Caffe",
                principalTable: "Caffe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRecord_User_UserRef",
                table: "FavoriteRecord",
                column: "UserRef",
                principalTable: "User",
                principalColumn: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_User_User1Ref",
                table: "Friend",
                column: "User1Ref",
                principalTable: "User",
                principalColumn: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Interested_User_AttenderRef",
                table: "Interested",
                column: "AttenderRef",
                principalTable: "User",
                principalColumn: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Interested_Event_EventRef",
                table: "Interested",
                column: "EventRef",
                principalTable: "Event",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PendingRequest_User_UserReceavedRef",
                table: "PendingRequest",
                column: "UserReceavedRef",
                principalTable: "User",
                principalColumn: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_PendingRequest_User_UserSentRef",
                table: "PendingRequest",
                column: "UserSentRef",
                principalTable: "User",
                principalColumn: "Username");
        }
    }
}
