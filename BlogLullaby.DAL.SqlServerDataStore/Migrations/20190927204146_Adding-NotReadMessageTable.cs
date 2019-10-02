using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogLullaby.BlogLullaby.DAL.SqlServerDataStore.Migrations
{
    public partial class AddingNotReadMessageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_UserProfiles_UserProfileId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "UserProfileId",
                table: "Messages",
                newName: "SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_UserProfileId",
                table: "Messages",
                newName: "IX_Messages_SenderId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastVisit",
                table: "UserProfiles",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 27, 23, 41, 43, 605, DateTimeKind.Local).AddTicks(6397),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 19, 3, 7, 53, 401, DateTimeKind.Local).AddTicks(6947));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 27, 23, 41, 43, 776, DateTimeKind.Local).AddTicks(7990),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 19, 3, 7, 53, 510, DateTimeKind.Local).AddTicks(2182));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Messages",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 27, 23, 41, 43, 792, DateTimeKind.Local).AddTicks(8829),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 19, 3, 7, 53, 527, DateTimeKind.Local).AddTicks(1710));

            migrationBuilder.CreateTable(
                name: "NotReadMessages",
                columns: table => new
                {
                    MessageId = table.Column<string>(nullable: false),
                    RecipientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotReadMessages", x => new { x.MessageId, x.RecipientId });
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_UserProfiles_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_UserProfiles_SenderId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "NotReadMessages");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Messages",
                newName: "UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                newName: "IX_Messages_UserProfileId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastVisit",
                table: "UserProfiles",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 19, 3, 7, 53, 401, DateTimeKind.Local).AddTicks(6947),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 27, 23, 41, 43, 605, DateTimeKind.Local).AddTicks(6397));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 19, 3, 7, 53, 510, DateTimeKind.Local).AddTicks(2182),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 27, 23, 41, 43, 776, DateTimeKind.Local).AddTicks(7990));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Messages",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 19, 3, 7, 53, 527, DateTimeKind.Local).AddTicks(1710),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 27, 23, 41, 43, 792, DateTimeKind.Local).AddTicks(8829));

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_UserProfiles_UserProfileId",
                table: "Messages",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
