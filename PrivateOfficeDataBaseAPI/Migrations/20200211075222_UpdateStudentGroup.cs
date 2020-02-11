using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrivateOfficeDataBaseAPI.Migrations
{
    public partial class UpdateStudentGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Group_IdGroup",
                table: "Classes");

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountStudents",
                table: "Group",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateClasses",
                table: "Classes",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Group_IdGroup",
                table: "Classes",
                column: "IdGroup",
                principalTable: "Group",
                principalColumn: "IdGroup",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Group_IdGroup",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "CountStudents",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "DateClasses",
                table: "Classes");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Group_IdGroup",
                table: "Classes",
                column: "IdGroup",
                principalTable: "Group",
                principalColumn: "IdGroup",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
