using Microsoft.EntityFrameworkCore.Migrations;

namespace PrivateOfficeWebApp.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Group_IdGroup",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_Group_IdGroup",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_Classes_IdClasses",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitedStudents_Classes_IdClasses",
                table: "VisitedStudents");

            migrationBuilder.DropColumn(
                name: "CountStudents",
                table: "Group");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Group_IdGroup",
                table: "Classes",
                column: "IdGroup",
                principalTable: "Group",
                principalColumn: "IdGroup",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Group_IdGroup",
                table: "Course",
                column: "IdGroup",
                principalTable: "Group",
                principalColumn: "IdGroup",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Classes_IdClasses",
                table: "Report",
                column: "IdClasses",
                principalTable: "Classes",
                principalColumn: "IdClasses",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitedStudents_Classes_IdClasses",
                table: "VisitedStudents",
                column: "IdClasses",
                principalTable: "Classes",
                principalColumn: "IdClasses",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Group_IdGroup",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_Group_IdGroup",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_Classes_IdClasses",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitedStudents_Classes_IdClasses",
                table: "VisitedStudents");

            migrationBuilder.AddColumn<int>(
                name: "CountStudents",
                table: "Group",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Group_IdGroup",
                table: "Classes",
                column: "IdGroup",
                principalTable: "Group",
                principalColumn: "IdGroup",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Group_IdGroup",
                table: "Course",
                column: "IdGroup",
                principalTable: "Group",
                principalColumn: "IdGroup",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Classes_IdClasses",
                table: "Report",
                column: "IdClasses",
                principalTable: "Classes",
                principalColumn: "IdClasses",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitedStudents_Classes_IdClasses",
                table: "VisitedStudents",
                column: "IdClasses",
                principalTable: "Classes",
                principalColumn: "IdClasses",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
