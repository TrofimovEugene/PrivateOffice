using Microsoft.EntityFrameworkCore.Migrations;

namespace PrivateOfficeDataBaseAPI.Migrations
{
    public partial class UpdateLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdClasses",
                table: "Report",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Report_IdClasses",
                table: "Report",
                column: "IdClasses");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Classes_IdClasses",
                table: "Report",
                column: "IdClasses",
                principalTable: "Classes",
                principalColumn: "IdClasses",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_Classes_IdClasses",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_Report_IdClasses",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "IdClasses",
                table: "Report");
        }
    }
}
