using Microsoft.EntityFrameworkCore.Migrations;

namespace PrivateOfficeDataBaseAPI.Migrations
{
    public partial class UpdateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Group_IdGroup",
                table: "Classes");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Group_IdGroup",
                table: "Classes",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Group_IdGroup",
                table: "Classes",
                column: "IdGroup",
                principalTable: "Group",
                principalColumn: "IdGroup",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
