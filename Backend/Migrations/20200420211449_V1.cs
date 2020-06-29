using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekat.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Record",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    DisplayImg = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    Demo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Record", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    ProfileImg = table.Column<string>(nullable: true),
                    Bio = table.Column<string>(nullable: true),
                    IsOwner = table.Column<int>(nullable: false),
                    IsAdmin = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "Caffe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    BackgroundImg = table.Column<string>(nullable: true),
                    OwnerRef = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caffe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Caffe_User_OwnerRef",
                        column: x => x.OwnerRef,
                        principalTable: "User",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoundRecordRef = table.Column<int>(nullable: false),
                    OwnerRef = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Record_BoundRecordRef",
                        column: x => x.BoundRecordRef,
                        principalTable: "Record",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comment_User_OwnerRef",
                        column: x => x.OwnerRef,
                        principalTable: "User",
                        principalColumn: "Username");
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Owner = table.Column<string>(nullable: true),
                    InterestedRef = table.Column<string>(nullable: true),
                    DateAndTime = table.Column<string>(nullable: true),
                    Modifier = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_User_Owner",
                        column: x => x.Owner,
                        principalTable: "User",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteRecord",
                columns: table => new
                {
                    UserRef = table.Column<string>(nullable: false),
                    RecordRef = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteRecord", x => new { x.UserRef, x.RecordRef });
                    table.ForeignKey(
                        name: "FK_FavoriteRecord_User_UserRef",
                        column: x => x.UserRef,
                        principalTable: "User",
                        principalColumn: "Username");
                });

            migrationBuilder.CreateTable(
                name: "Friend",
                columns: table => new
                {
                    User1Ref = table.Column<string>(nullable: false),
                    User2Ref = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friend", x => new { x.User1Ref, x.User2Ref });
                    table.ForeignKey(
                        name: "FK_Friend_User_User1Ref",
                        column: x => x.User1Ref,
                        principalTable: "User",
                        principalColumn: "Username");
                });

            migrationBuilder.CreateTable(
                name: "PendingRequest",
                columns: table => new
                {
                    UserSentRef = table.Column<string>(nullable: false),
                    UserReceavedRef = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingRequest", x => new { x.UserSentRef, x.UserReceavedRef });
                    table.ForeignKey(
                        name: "FK_PendingRequest_User_UserReceavedRef",
                        column: x => x.UserReceavedRef,
                        principalTable: "User",
                        principalColumn: "Username");
                    table.ForeignKey(
                        name: "FK_PendingRequest_User_UserSentRef",
                        column: x => x.UserSentRef,
                        principalTable: "User",
                        principalColumn: "Username");
                });

            migrationBuilder.CreateTable(
                name: "Interested",
                columns: table => new
                {
                    EventRef = table.Column<int>(nullable: false),
                    AttenderRef = table.Column<string>(nullable: false),
                    AttendantRef = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interested", x => new { x.EventRef, x.AttenderRef });
                    table.ForeignKey(
                        name: "FK_Interested_User_AttenderRef",
                        column: x => x.AttenderRef,
                        principalTable: "User",
                        principalColumn: "Username");
                    table.ForeignKey(
                        name: "FK_Interested_Event_EventRef",
                        column: x => x.EventRef,
                        principalTable: "Event",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Caffe_OwnerRef",
                table: "Caffe",
                column: "OwnerRef");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_BoundRecordRef",
                table: "Comment",
                column: "BoundRecordRef");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_OwnerRef",
                table: "Comment",
                column: "OwnerRef");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Owner",
                table: "Event",
                column: "Owner");

            migrationBuilder.CreateIndex(
                name: "IX_Interested_AttenderRef",
                table: "Interested",
                column: "AttenderRef");

            migrationBuilder.CreateIndex(
                name: "IX_PendingRequest_UserReceavedRef",
                table: "PendingRequest",
                column: "UserReceavedRef");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caffe");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "FavoriteRecord");

            migrationBuilder.DropTable(
                name: "Friend");

            migrationBuilder.DropTable(
                name: "Interested");

            migrationBuilder.DropTable(
                name: "PendingRequest");

            migrationBuilder.DropTable(
                name: "Record");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
