using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogLullaby.BlogLullaby.DAL.SqlServerDataStore.Migrations
{
    public partial class CheckWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Specialization",
                table: "UserProfiles",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastVisit",
                table: "UserProfiles",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 19, 3, 7, 53, 401, DateTimeKind.Local).AddTicks(6947),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 19, 2, 52, 57, 175, DateTimeKind.Local).AddTicks(2857));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 19, 3, 7, 53, 510, DateTimeKind.Local).AddTicks(2182),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 19, 2, 52, 57, 340, DateTimeKind.Local).AddTicks(6735));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Messages",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 19, 3, 7, 53, 527, DateTimeKind.Local).AddTicks(1710),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 19, 2, 52, 57, 354, DateTimeKind.Local).AddTicks(4844));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Specialization",
                table: "UserProfiles",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastVisit",
                table: "UserProfiles",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 19, 2, 52, 57, 175, DateTimeKind.Local).AddTicks(2857),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 19, 3, 7, 53, 401, DateTimeKind.Local).AddTicks(6947));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 19, 2, 52, 57, 340, DateTimeKind.Local).AddTicks(6735),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 19, 3, 7, 53, 510, DateTimeKind.Local).AddTicks(2182));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Messages",
                nullable: false,
                defaultValue: new DateTime(2019, 9, 19, 2, 52, 57, 354, DateTimeKind.Local).AddTicks(4844),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 9, 19, 3, 7, 53, 527, DateTimeKind.Local).AddTicks(1710));
        }
    }
}
