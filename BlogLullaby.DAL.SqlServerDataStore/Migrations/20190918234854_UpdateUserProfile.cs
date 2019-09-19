using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogLullaby.WEB_API.Migrations
{
    public partial class UpdateUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_Username",
                table: "UserProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "UserProfiles",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastVisit",
                table: "UserProfiles",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 19, 2, 48, 52, 921, DateTimeKind.Local).AddTicks(349),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 19, 2, 37, 47, 377, DateTimeKind.Local).AddTicks(6763));

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "UserProfiles",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "UserProfiles",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 19, 2, 48, 53, 82, DateTimeKind.Local).AddTicks(8011),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 19, 2, 37, 47, 605, DateTimeKind.Local).AddTicks(1359));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Messages",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 19, 2, 48, 53, 119, DateTimeKind.Local).AddTicks(3649),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 19, 2, 37, 47, 622, DateTimeKind.Local).AddTicks(5699));

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_Username",
                table: "UserProfiles",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_Username",
                table: "UserProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "UserProfiles",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastVisit",
                table: "UserProfiles",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 19, 2, 37, 47, 377, DateTimeKind.Local).AddTicks(6763),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 19, 2, 48, 52, 921, DateTimeKind.Local).AddTicks(349));

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "UserProfiles",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "UserProfiles",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 19, 2, 37, 47, 605, DateTimeKind.Local).AddTicks(1359),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 19, 2, 48, 53, 82, DateTimeKind.Local).AddTicks(8011));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Messages",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 19, 2, 37, 47, 622, DateTimeKind.Local).AddTicks(5699),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 19, 2, 48, 53, 119, DateTimeKind.Local).AddTicks(3649));

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_Username",
                table: "UserProfiles",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");
        }
    }
}
