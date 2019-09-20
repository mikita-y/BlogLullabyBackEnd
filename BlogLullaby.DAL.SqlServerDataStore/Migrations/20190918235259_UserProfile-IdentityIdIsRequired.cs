using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogLullaby.BlogLullaby.DAL.SqlServerDataStore.Migrations
{
    public partial class UserProfileIdentityIdIsRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastVisit",
                table: "UserProfiles",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 19, 2, 52, 57, 175, DateTimeKind.Local).AddTicks(2857),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 19, 2, 48, 52, 921, DateTimeKind.Local).AddTicks(349));

            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserId",
                table: "UserProfiles",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 19, 2, 52, 57, 340, DateTimeKind.Local).AddTicks(6735),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 19, 2, 48, 53, 82, DateTimeKind.Local).AddTicks(8011));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Messages",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 19, 2, 52, 57, 354, DateTimeKind.Local).AddTicks(4844),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 19, 2, 48, 53, 119, DateTimeKind.Local).AddTicks(3649));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastVisit",
                table: "UserProfiles",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 19, 2, 48, 52, 921, DateTimeKind.Local).AddTicks(349),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 19, 2, 52, 57, 175, DateTimeKind.Local).AddTicks(2857));

            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserId",
                table: "UserProfiles",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 19, 2, 48, 53, 82, DateTimeKind.Local).AddTicks(8011),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 19, 2, 52, 57, 340, DateTimeKind.Local).AddTicks(6735));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Messages",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 19, 2, 48, 53, 119, DateTimeKind.Local).AddTicks(3649),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 19, 2, 52, 57, 354, DateTimeKind.Local).AddTicks(4844));
        }
    }
}
