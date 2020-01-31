using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrivateOfficeDataBaseAPI.Migrations
{
    public partial class CourseNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Course",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NameUniversity",
                table: "Course",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Course",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CountClasses",
                table: "Classes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DaysWeek",
                table: "Classes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "NameUniversity",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "CountClasses",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "DaysWeek",
                table: "Classes");
        }
    }
}
