using Microsoft.EntityFrameworkCore.Migrations;

namespace PrivateOfficeDataBaseAPI.Migrations
{
    public partial class UpdateGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Classes_IdClasses",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Group_IdClasses",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "IdClasses",
                table: "Group");

            migrationBuilder.AddColumn<int>(
                name: "IdGroup",
                table: "Course",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdGroup",
                table: "Classes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Course_IdGroup",
                table: "Course",
                column: "IdGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_IdGroup",
                table: "Classes",
                column: "IdGroup");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Group_IdGroup",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_Group_IdGroup",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_IdGroup",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Classes_IdGroup",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "IdGroup",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "IdGroup",
                table: "Classes");

            migrationBuilder.AddColumn<int>(
                name: "IdClasses",
                table: "Group",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Group_IdClasses",
                table: "Group",
                column: "IdClasses");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Classes_IdClasses",
                table: "Group",
                column: "IdClasses",
                principalTable: "Classes",
                principalColumn: "IdClasses",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
